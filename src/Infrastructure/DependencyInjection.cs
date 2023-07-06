using AutoMapper.Configuration;
using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Infrastructure.Files;
using ElarabyCA.Infrastructure.Identity;
using ElarabyCA.Infrastructure.Persistence;
using ElarabyCA.Infrastructure.Services;
using ElarabyCA.WebUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ElarabyCA.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            Microsoft.Extensions.Configuration.IConfiguration configuration, IHostEnvironment env)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ElarabyCADb"));
            }
            else
            {
                //services.AddDbContext<ApplicationDbContext>(options =>
                //    options.UseSqlServer(
                //        configuration.GetConnectionString("DefaultConnection"), opt =>
                //        {
                //            opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                //            opt.EnableRetryOnFailure();
                //        }));

                //string mySqlConnectionStr = configuration.GetConnectionString("MySqlConnection");
                //services.AddDbContext<ApplicationDbContext>(options =>
                //    options.UseMySQL(mySqlConnectionStr, opt =>
                //    {
                //        opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                //    }));

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("SqlLiteConnection")));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            var cert = new X509Certificate2(Path.Combine(env.ContentRootPath + "\\wwwroot\\certificate\\", "certificate.pfx"), "gaboub");
            services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddProfileService<ProfileService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<ElarabyRESTService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            //.AddFacebook(facebookOptions =>
            //    {
            //        facebookOptions.AppId = "2430622170514507";
            //        facebookOptions.AppSecret = "ccf6c0b0ca74af40abda49d4987acd59";

            //        //facebookOptions.Scope.Add("user_birthday");
            //        facebookOptions.Scope.Add("public_profile");

            //        //facebookOptions.Fields.Add("birthday");
            //        facebookOptions.Fields.Add("picture");
            //        facebookOptions.Fields.Add("name");
            //        facebookOptions.Fields.Add("email");
            //        facebookOptions.Fields.Add("gender");

            //        //facebookOptions.Events = new OAuthEvents
            //        //{
            //        //    OnCreatingTicket = context =>
            //        //    {
            //        //        var identity = (ClaimsIdentity)context.Principal.Identity;
            //        //        //var profileImg = context.User["picture"]["data"].Value<string>("url");
            //        //        identity.AddClaim(new Claim(JwtClaimTypes.Picture, ""));
            //        //        return Task.CompletedTask;
            //        //    }
            //        //};
            //    });

            return services;
        }
    }
}
