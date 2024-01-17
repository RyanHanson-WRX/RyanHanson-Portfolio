using Microsoft.EntityFrameworkCore;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.Services;
using DoughnutDreamsBrewedBeans.DAL.Abstract;
using DoughnutDreamsBrewedBeans.DAL.Concrete;

namespace DoughnutDreamsBrewedBeans;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var connectionString = builder.Configuration.GetConnectionString("DDBBConnection");
        builder.Services.AddDbContext<DDBBDbContext>(
            options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(connectionString));
        builder.Services.AddScoped<DbContext, DDBBDbContext>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
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
