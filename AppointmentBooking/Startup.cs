using AppointmentBooking.Data;
using AppointmentBooking.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentBooking
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
            services.AddDbContext<AppointmentDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GoogleOpenID";
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";

                }).AddOpenIdConnect("GoogleOpenID", options =>
                {
                    options.Authority = "https://accounts.google.com";
                    options.ClientId = "562521670675-92vule9iisl7p2aau6murjf9jrcnm9mn.apps.googleusercontent.com";
                    options.ClientSecret = "WoDy4M33--ERUh2xgM6f666L";
                    options.CallbackPath = "/auth";
                });

                //.AddGoogle(options => {
                //    options.ClientId = "562521670675-92vule9iisl7p2aau6murjf9jrcnm9mn.apps.googleusercontent.com";
                //    options.ClientSecret = "WoDy4M33--ERUh2xgM6f666L";
                //    options.CallbackPath = "/auth";
                //    options.AuthorizationEndpoint += "?prompt=consent";
                //});
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    //initializing custom roles 
        //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var UserManager = serviceProvider.GetRequiredService<UserManager<AdminModel>>();
        //    string[] roleNames = { "Admin", "Store-Manager", "Member" };
        //    IdentityResult roleResult;

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await RoleManager.RoleExistsAsync(roleName);
        //        // ensure that the role does not exist
        //        if (!roleExist)
        //        {
        //            //create the roles and seed them to the database: 
        //            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }

        //    // find the user with the admin email 
        //    var _user = await UserManager.FindByEmailAsync("admin@email.com");

        //    // check if the user exists
        //    if (_user == null)
        //    {
        //        //Here you could create the super admin who will maintain the web app

        //        var poweruser = new AdminModel
        //        {
        //            Username = "Admin",
        //            Email = "admin@email.com",
        //        };
        //        string adminPassword = "password";

        //        var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
        //        if (createPowerUser.Succeeded)
        //        {
        //            //here we tie the new user to the role
        //            await UserManager.AddToRoleAsync(poweruser, "Admin");

        //        }
        //    }
        //}
    }
}
