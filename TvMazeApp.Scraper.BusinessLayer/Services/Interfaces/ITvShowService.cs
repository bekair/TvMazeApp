using TvMazeApp.Scraper.BusinessLayer.Models.Responses;

namespace TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

public interface ITvShowService
{
    Task<TvShowAddResponse> AddShowsByNameWithEpisodesAsync(string? showName);
}