using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    public class StudentAuthManager : IAuthService<Student,StudentForLoginDto,StudentForRegisterDto>
    {
        private IUserService<Student>_userService;
        private ITokenHelper _tokenHelper;

        public StudentAuthManager(IUserService<Student> userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(StudentValidator))]
        public IDataResult<Student> Register(StudentForRegisterDto userForRegisterDto, string password)
        {
            //IResult result = BusinessRules.Run(
            //    CheckIfTurkisCitizen(userForRegisterDto)
            //);
            //if (!result.Success)
            //{
            //    return new ErrorDataResult<Student>(null, "Türk vatandaşı değil");
            //}
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var student = new Student
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName.ToUpper(),
                LastName = userForRegisterDto.LastName.ToUpper(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                TcIdentity = userForRegisterDto.TcIdentity,

                BirthYear = userForRegisterDto.BirthYear,
                IsActive = true
            };
            _userService.Add(student);
            return new SuccessDataResult<Student>(student, Messages.UserRegistered);
        }

        public IDataResult<Student> Login(StudentForLoginDto studentForLoginDto)
        {
            var userToCheck = _userService.GetByIdentity(studentForLoginDto.TcIdentitiy);
            if (userToCheck == null)
            {
                return new ErrorDataResult<Student>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(studentForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Student>(Messages.PasswordError);
            }

            return new SuccessDataResult<Student>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(long Tc)
        {
            if (_userService.GetByIdentity(Tc) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(Student enity)
        {
            var claims = _userService.GetClaims(enity);
            var accessToken = _tokenHelper.CreateToken(enity, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        private IResult CheckIfTurkisCitizen(StudentForRegisterDto registerDto)
        {
            KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var result = client.TCKimlikNoDogrula(registerDto.TcIdentity, registerDto.FirstName, registerDto.LastName, registerDto.BirthYear);

            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<Student> ChangePassword(long Tc, string OldPassword, string NewPassword)
        {
            var result = UserExists(Tc);
            if (result.Success) { return new ErrorDataResult<Student>(Messages.UserNotFound); }
            var userToCheck = _userService.GetByIdentity(Tc);
            if (!HashingHelper.VerifyPasswordHash(OldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Student>(Messages.PasswordError);
            }
            if (OldPassword == NewPassword)
            {
                return new ErrorDataResult<Student>("Yeni şifre eskisiyle aynı olamaz");
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(NewPassword, out passwordHash, out passwordSalt);

            Student student = _userService.GetByIdentity(Tc);
            student.TcIdentity = userToCheck.TcIdentity;
            student.Email = userToCheck.Email;
            student.FirstName = userToCheck.FirstName;
            student.LastName = userToCheck.LastName;
            student.PasswordHash = passwordHash;
            student.PasswordSalt = passwordSalt;
            student.BirthYear = userToCheck.BirthYear;
            student.IsActive = userToCheck.IsActive;

            _userService.UpdateUser(student);
            return new SuccessDataResult<Student>(student, Messages.UserUpdated);
        }

    }
}
