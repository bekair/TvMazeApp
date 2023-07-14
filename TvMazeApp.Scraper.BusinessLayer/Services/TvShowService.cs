using TvMazeApp.Core.Constants;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

namespace TvMazeApp.Scraper.BusinessLayer.Services;

public class TvShowService : ITvShowService
{
    private readonly IApiCaller _apiCaller;
    private readonly ITvShowRepository _tvShowRepository;

    public TvShowService(
        IApiCaller apiCaller, 
        ITvShowRepository tvShowRepository)
    {
        _apiCaller = apiCaller ?? throw new ArgumentNullException(nameof(apiCaller));
        _tvShowRepository = tvShowRepository ?? throw new ArgumentNullException(nameof(tvShowRepository));
    }

    public async Task AddShowsByNameWithEpisodesAsync(string? showName)
    {
        if(string.IsNullOrWhiteSpace(showName))
            throw new ArgumentException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);

        var showsResponse = await _apiCaller.GetTvShowsByNameAsync( string.Format(ScraperConstant.Uri.SearchShowUri, showName));
        foreach (var response in showsResponse)
        {
            var showWithEpisodes = _apiCaller.GetTvShowsWithEpisodesByShowId(string.Format(ScraperConstant.Uri.ShowEpisodeListUri, response.TvShow?.Href));
        }
    }
}