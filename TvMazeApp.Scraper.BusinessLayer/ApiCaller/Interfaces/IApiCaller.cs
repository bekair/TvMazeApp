using TvMazeApp.Scraper.BusinessLayer.Models;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses;

namespace TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;

public interface IApiCaller
{
    Task<ICollection<TvShowApiResponse>> GetTvShowsByNameAsync(string uri);
    Task<ShowEpisodesApiResponse?> GetTvShowsWithEpisodesByShowId(string uri);
    Task<TvShowModelWithEpisodes> GetTvShowWithEpisodesByApiId(int apiId);
}