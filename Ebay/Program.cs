using Application.Extensions;
using Application.Interfaces.IServices;
using Application.Services;
using Ebay.Hubs;
using Mapster;

namespace Ebay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.ConfigDatabaseInject(builder.Configuration);
            builder.Services.AddMapster();     
            builder.Services.AddServices();
            builder.Services.AddSignalR();   
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(10);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
   
            app.UseAuthorization();
            app.MapHub<ChatHub>("/chathub");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
