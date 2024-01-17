using JustWatchStreams.DAL.Abstract;
using JustWatchStreams.DAL.Concrete;
using JustWatchStreams.Models;
using Microsoft.EntityFrameworkCore;

namespace JustWatchStreams;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var connectionString = builder.Configuration.GetConnectionString("RyansConnection");
        builder.Services.AddDbContext<JustWatchDbContext>(
                options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString));
        builder.Services.AddScoped<DbContext, JustWatchDbContext>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IShowRepository, ShowRepository>();
        builder.Services.AddScoped<ICreditRepository, CreditRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();

        // builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c => {
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();
            c.CustomSchemaIds(type => type.FullName);
        });

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
