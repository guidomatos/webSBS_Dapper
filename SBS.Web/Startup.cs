using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SBS.ApplicationCore.Interfaces.Repositories;
using SBS.ApplicationCore.Interfaces.Services;
using SBS.ApplicationCore.Services;
using SBS.Infrastructure.Repositories;

namespace SBS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationName = Configuration["AppSettings:ApplicationName"];
            var sessionTimeout = Configuration["AppSettings:SessionTimeout"];

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.Cookie.Name = string.Format("{0}.Session", applicationName);
                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(sessionTimeout));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = string.Format("{0}.Auth", applicationName);
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(sessionTimeout));
                    options.LoginPath = new PathString("/Login");
                    options.AccessDeniedPath = new PathString("/Login/Forbidden/");
                });

            // Repositories
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

            // Service
            services.AddSingleton<IUsuarioService, UsuarioService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSession();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
