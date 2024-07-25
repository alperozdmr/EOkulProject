using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependancyResolvers.Autofac;
using Castle.Core.Configuration;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Core.Exstensions;
//using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//******.NET6 DA AUTOFAC BU �EK�LDE KULLANILIYOR******
//yukar�daki addsingelton yerine bussiners katman�nda
// olu�turdu�umuz autofacbussines class�nda yaz�yoruz
// katmanlar aras�ndaki ba��ml�klar� kontrol etmek i�im bu bir IoC yap�s�d�r 
// visiual studionun default IoC yap�s� olmas�na ra�men biz autofac yap�s�ylada bu ba��ml�l�kalr� kontrol edebiliriz
builder.Services.AddHttpContextAccessor();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // �� hizmetlerinizi burada kaydedin    
    builder.RegisterModule(new AutofacBusinessModule());
    //BU �EK�LDEDE YAZILAB�L�R
    //builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
    //builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
});
//******.NET6 DA AUTOFAC BU �EK�LDE KULLANILIYOR******
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
