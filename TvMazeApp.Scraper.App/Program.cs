using Microsoft.EntityFrameworkCore;
using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Implementations;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.Scraper.App.Settings;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Services;
using TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TvMazeSettings>(builder.Configuration.GetSection(nameof(TvMazeSettings)));
var tvMazeSettings = new TvMazeSettings();
var tvMazeConfigSection = builder.Configuration.GetSection(nameof(TvMazeSettings));
tvMazeConfigSection.Bind(tvMazeSettings);

var httpClient = new HttpClient
{
    BaseAddress = new Uri(tvMazeSettings.ApiBaseUri)
};

// Add services to the container.
builder.Services.AddSingleton(httpClient);
builder.Services.AddScoped<ITvShowRepository, TvShowRepository>();
builder.Services.AddScoped<IApiCaller, ApiCaller>();
builder.Services.AddScoped<ITvShowService, TvShowService>();
builder.Services.AddDbContext<TvMazeContext>(options => options.UseSqlServer(tvMazeSettings.ConnectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
