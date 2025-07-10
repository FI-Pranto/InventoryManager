using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Application.Services;
using InventoryManager.Infrastructure.Data;
using InventoryManager.Infrastructure.Repositories;
using InventoryManager.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DC")));

            //repositories
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            //unitOfWork
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            //services
            builder.Services.AddScoped<IUnitService, UnitService>();

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
