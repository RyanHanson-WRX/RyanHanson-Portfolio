using HW4.Services;
using System.Net.Http.Headers;

namespace HW4;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string tmdbApiKey = builder.Configuration["TMDBApiKey"];
        string tmdbApiUrl = "https://api.themoviedb.org/3/";

        builder.Services.AddHttpClient<ITMDBService, TMDBService>((httpClient, services) =>
        {
            httpClient.BaseAddress = new Uri(tmdbApiUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "CS460-HW4");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbApiKey);
            return new TMDBService(httpClient, services.GetRequiredService<ILogger<TMDBService>>());
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

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
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
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
