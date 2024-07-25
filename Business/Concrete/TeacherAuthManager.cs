using Business.Abstract;
using Business.Constants;
using Castle.Core.Resource;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TeacherAuthManager : IAuthService<Teacher,TeacherForLoginDto,TeacherForRegisterDto>
    {
        private IUserService<Teacher> _userService;
        private ITokenHelper _tokenHelper;

        public TeacherAuthManager(IUserService<Teacher> userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<Teacher> Register(TeacherForRegisterDto userForRegisterDto, string password)
        {
            IResult result = BusinessRules.Run(
                CheckIfTurkisCitizen(userForRegisterDto)
            );
            if (!result.Success )
            {
                return new ErrorDataResult<Teacher>(null,"Türk vatandaşı değil");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var teacher = new Teacher
            {
                TcIdentity=userForRegisterDto.TcIdentity,
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName.ToUpper(),
                LastName = userForRegisterDto.LastName.ToUpper(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserName = userForRegisterDto.UserName,
                BirthYear = userForRegisterDto.BirthYear,   
                IsActive = true
            };
            _userService.Add(teacher);
            return new SuccessDataResult<Teacher>(teacher, Messages.UserRegistered);
        }

        public IDataResult<Teacher> Login(TeacherForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByUsername(userForLoginDto.UserNanme);
            if (userToCheck == null)
            {
                return new ErrorDataResult<Teacher>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Teacher>(Messages.PasswordError);
            }

            return new SuccessDataResult<Teacher>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(long Tc)
        {
            if (_userService.GetByIdentity(Tc) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
        public IDataResult<Teacher> ChangePassword(long Tc, string OldPassword,string NewPassword) {
            var result = UserExists(Tc);
            if (result.Success) { return new ErrorDataResult<Teacher>(Messages.UserNotFound); }
            var userToCheck = _userService.GetByIdentity(Tc);
            if (!HashingHelper.VerifyPasswordHash(OldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Teacher>(Messages.PasswordError);
            }
            if(OldPassword==NewPassword)
            {
                return new ErrorDataResult<Teacher>("Yeni şifre eskisiyle aynı olamaz");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(NewPassword, out passwordHash, out passwordSalt);

            Teacher teacher = _userService.GetByIdentity(Tc);
            teacher.TcIdentity= userToCheck.TcIdentity;
            teacher.Email= userToCheck.Email;
            teacher.FirstName= userToCheck.FirstName;
            teacher.LastName= userToCheck.LastName; 
            teacher.PasswordHash= passwordHash;
            teacher.PasswordSalt= passwordSalt;
            teacher.UserName= userToCheck.UserName;
            teacher.BirthYear= userToCheck.BirthYear;
            teacher.IsActive= userToCheck.IsActive;

            _userService.UpdateUser(teacher);
            return new SuccessDataResult<Teacher>(teacher, Messages.UserUpdated);
        }

        public IDataResult<AccessToken> CreateAccessToken(Teacher teacher)
        {
            var claims = _userService.GetClaims(teacher);
            var accessToken = _tokenHelper.CreateToken(teacher, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        private IResult CheckIfTurkisCitizen(TeacherForRegisterDto registerDto)
        {
            KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var result = client.TCKimlikNoDogrula(registerDto.TcIdentity, registerDto.FirstName, registerDto.LastName, registerDto.BirthYear);

            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }


    }
}
