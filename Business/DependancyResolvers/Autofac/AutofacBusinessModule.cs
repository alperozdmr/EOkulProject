using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependancyResolvers.Autofac
{
    public class AutofacBusinessModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentAuthManager>().As<IAuthService<Student,StudentForLoginDto,StudentForRegisterDto>>().SingleInstance();
            builder.RegisterType<EfStudentDal>().As<IStudentDal>().SingleInstance();
            builder.RegisterType<TeacherAuthManager>().As<IAuthService<Teacher, TeacherForLoginDto, TeacherForRegisterDto>>().SingleInstance();
            builder.RegisterType<EfTeacherDal>().As<ITeacherDal>().SingleInstance();

            builder.RegisterType<StudentManager>().As<IUserService<Student>>().SingleInstance();
            builder.RegisterType<TeacherManager>().As<IUserService<Teacher>>().SingleInstance();

            builder.RegisterType<StudentNoteManager>().As<IStudentNoteService>().SingleInstance();
            builder.RegisterType<EfStudentNoteDal>().As<IStudentNoteDal>().SingleInstance();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance(); ;



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
