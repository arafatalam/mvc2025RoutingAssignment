using mvc2025RoutingAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace mvc2025RoutingAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            
            builder.Services.AddDbContext<ApplicationDbContext>(options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("BlogConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();



            app.MapControllerRoute(
                name: "tagSearch",
                pattern: "Search/{nameOfTag?}",
                defaults: new { controller = "Tags", action = "Search" });

            app.MapControllerRoute(
                name: "archiveSearch",
                pattern: "Archive/{entryDate?}",
                defaults: new { controller = "Archive", action = "Entry" });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
