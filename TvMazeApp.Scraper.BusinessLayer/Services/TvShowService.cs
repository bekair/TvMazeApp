using AutoMapper;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Entity;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses;
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

    public async Task<TvShowAddResponse> AddShowsByNameWithEpisodesAsync(string? showName)
    {
        if (string.IsNullOrWhiteSpace(showName))
            throw new ParameterException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);

        var showsResponse = await _apiCaller.GetTvShowsByNameAsync(string.Format(ScraperConstant.Uri.SearchShowUriByShowName, showName));
        showsResponse = await GetNotExistedTvShowsAsync(showsResponse);
        if (!showsResponse.Any())
            return new TvShowAddResponse(
                AppConstant.InfoMessage.AlreadyCreatedTvShows,
                Severity.Info
            );
        
        foreach (var response in showsResponse)
        {
            var showWithEpisodes = await _apiCaller.GetTvShowsWithEpisodesByShowId(
                string.Format(ScraperConstant.Uri.ShowEpisodeListUri, response.TvShow?.Links?.Self?.Href));

            var tvShow = Mapper.Map<TvShow>(response.TvShow);
            var episodes = Mapper.Map<IEnumerable<Episode>>(showWithEpisodes?.EmbeddedEpisodeModel?.Episodes);
            tvShow.Episodes = episodes;

            UnitOfWork.TvShowRepository.AddTvShowWithEpisodes(tvShow);
        }

        await UnitOfWork.CommitAsync();
        
        return new TvShowAddResponse(
            AppConstant.SuccessMessage.SavedTvShowsSuccessfully,
            Severity.Success
        );
    }

    private async Task<ICollection<TvShowApiResponse>> GetNotExistedTvShowsAsync(ICollection<TvShowApiResponse> showApiResponses)
    {
        var idList = showApiResponses.Select(x => x.TvShow?.Id);
        var existedTvShowIdList = (await UnitOfWork.TvShowRepository
            .GetAsync(show => idList.Contains(show.ApiId))
        ).Select(show => show.ApiId);

        return showApiResponses.Where(s => 
            s.TvShow != null && 
            !existedTvShowIdList.Contains(s.TvShow.Id)
        ).ToList();
    }
}