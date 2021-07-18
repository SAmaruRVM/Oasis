using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using System;

namespace Oasis.Web.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void Adicionar(this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddControllersWithViews();

            @this.AddDbContextPool<OasisContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("SQL-SERVER"))
            );

            @this.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<OasisContext>();


            @this.ConfigureApplicationCookie(cookieOptions =>
            {
                cookieOptions.Cookie.HttpOnly = true;
                cookieOptions.Cookie.Path = "/";
                cookieOptions.Cookie.IsEssential = true;
                cookieOptions.ExpireTimeSpan = TimeSpan.FromDays(366);
            });

            @this.AddHttpContextAccessor();
            @this.AddDistributedMemoryCache();
            
            @this.AddSession(sessionOptions =>
            {
                sessionOptions.Cookie.Name = Guid.NewGuid().ToString();
                sessionOptions.Cookie.HttpOnly = true;
                sessionOptions.Cookie.Path = "/";
                sessionOptions.Cookie.IsEssential = true;
                sessionOptions.IdleTimeout = TimeSpan.FromSeconds(10);
            });


            @this.AddRouting(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });
        }
    }
}
