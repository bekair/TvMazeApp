namespace TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

public interface ITvShowService
{
    Task AddShowsByNameWithEpisodesAsync(string? showName);
}