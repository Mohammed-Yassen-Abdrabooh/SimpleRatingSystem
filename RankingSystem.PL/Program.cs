using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RankingSystem.BLL.Interfaces;
using RankingSystem.BLL.Repositories;
using RankingSystem.DAL.DbContexts;
using RankingSystem.DAL.Models;
using RankingSystem.PL.MapppingProfilles;

namespace RankingSystem.PL
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<SimpleRatingSystemDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>( /*options =>
            {
                options.Password.RequireDigit = true;

            }*/)
                            .AddEntityFrameworkStores<SimpleRatingSystemDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(options =>
                            {
                                options.LoginPath = "Account/Login";
                                options.AccessDeniedPath = "Home/Error";
                            });
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure PipeLines
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}