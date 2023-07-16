using Microsoft.EntityFrameworkCore;
using TvMazeApp.API.BusinessLayer.Services;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.API.Middlewares;
using TvMazeApp.Core.Constants;
using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Implementations;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.DataAccess.UnitOfWorks;
using TvMazeApp.DataAccess.UnitOfWorks.Base;

var builder = WebApplication.CreateBuilder(args);

var connectionStringTvMaze = builder.Configuration.GetConnectionString("TvMazeConnection");
if (string.IsNullOrWhiteSpace(connectionStringTvMaze)) 
    throw new InvalidOperationException(AppConstant.ErrorMessage.TvMazeConnectionStringNotFound);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITvShowRepository, TvShowRepository>();
builder.Services.AddScoped<ITvShowsApiService, TvShowsApiService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<TvMazeContext>(options => options.UseSqlServer(connectionStringTvMaze));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.Run();