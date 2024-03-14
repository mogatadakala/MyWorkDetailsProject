using Microsoft.EntityFrameworkCore;
using MyWorkDetailsProject.Models;
using NLog.Extensions.Logging;
using NLog.Web;

namespace MyWorkDetailsProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options=>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
            );
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("https://localhost:7132/Home/");
                });
            });
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDBCOntext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LoginReg")));
            var app = builder.Build();
            app.Logger.LogInformation("Starting the app");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}