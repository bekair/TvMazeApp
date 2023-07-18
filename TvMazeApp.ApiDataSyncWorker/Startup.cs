using System;
using System.IO;
using System.Net.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMazeApp.ApiDataSyncWorker;
using TvMazeApp.ApiDataSyncWorker.Settings;
using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Implementations;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.DataAccess.UnitOfWorks;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TvMazeApp.ApiDataSyncWorker
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var tvMazeSettings = new TvMazeSettings();
            config.Bind(nameof(TvMazeSettings), tvMazeSettings);

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(tvMazeSettings.ApiBaseUri)
            };

            builder.Services.AddDbContext<TvMazeContext>(options => options.UseSqlServer(tvMazeSettings.ConnectionString));
            builder.Services.AddSingleton(httpClient);
            builder.Services.AddScoped<IApiCaller, ApiCaller>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITvShowRepository, TvShowRepository>();
            builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
