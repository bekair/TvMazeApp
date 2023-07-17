using Microsoft.EntityFrameworkCore;
using TvMazeApp.Core.Constants;
using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Implementations;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.DataAccess.UnitOfWorks;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Scraper.App.Middlewares;
using TvMazeApp.Scraper.App.Settings;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Services;
using TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TvMazeSettings>(builder.Configuration.GetSection(nameof(TvMazeSettings)));

var tvMazeConfigSection = builder.Configuration.GetSection(nameof(TvMazeSettings));
if (tvMazeConfigSection == default) 
    throw new InvalidOperationException(string.Format(AppConstant.ErrorMessage.TvMazeSettingsNotFound, nameof(tvMazeConfigSection)));

var tvMazeSettings = new TvMazeSettings();
tvMazeConfigSection.Bind(tvMazeSettings);

var httpClient = new HttpClient
{
    BaseAddress = new Uri(tvMazeSettings.ApiBaseUri!)
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("TvMazeOrigin",
        cors =>
        {
            cors.WithOrigins("http://localhost:44477")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton(httpClient);
builder.Services.AddScoped<ITvShowRepository, TvShowRepository>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IApiCaller, ApiCaller>();
builder.Services.AddScoped<ITvShowService, TvShowService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<TvMazeContext>(options => options.UseSqlServer(tvMazeSettings.ConnectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TvMazeContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseCors("TvMazeOrigin");
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
