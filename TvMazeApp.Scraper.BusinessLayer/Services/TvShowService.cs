using AutoMapper;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Entity;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Services.Base;
using TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

namespace TvMazeApp.Scraper.BusinessLayer.Services;

public class TvShowService : ServiceBase, ITvShowService
{
    private readonly IApiCaller _apiCaller;

    public TvShowService(
        IApiCaller apiCaller, 
        IUnitOfWork unitOfWork,
        IMapper mapper) : base(unitOfWork, mapper)
    {
        _apiCaller = apiCaller ?? throw new ArgumentNullException(nameof(apiCaller));
    }

    public async Task AddShowsByNameWithEpisodesAsync(string? showName)
    {
        if(string.IsNullOrWhiteSpace(showName))
            throw new ParameterException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);

        var showsResponse = await _apiCaller.GetTvShowsByNameAsync( string.Format(ScraperConstant.Uri.SearchShowUri, showName));
        foreach (var response in showsResponse)
        {
            var showWithEpisodes = await _apiCaller.GetTvShowsWithEpisodesByShowId(string.Format(ScraperConstant.Uri.ShowEpisodeListUri, response.TvShow?.Links?.Self?.Href));
            
            var tvShow = Mapper.Map<TvShow>(response.TvShow);
            var episodes = Mapper.Map<IEnumerable<Episode>>(showWithEpisodes?.EmbeddedEpisodeModel?.Episodes);
            tvShow.Episodes = episodes;
            
            UnitOfWork.TvShowRepository.AddTvShowWithEpisodes(tvShow);
        }

        await UnitOfWork.CommitAsync();
    }
}