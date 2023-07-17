using System.Net;
using AutoMapper;
using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.API.BusinessLayer.Models.Dtos;
using TvMazeApp.API.BusinessLayer.Services.Base;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Enums;
using TvMazeApp.DataAccess.UnitOfWorks.Base;

namespace TvMazeApp.API.BusinessLayer.Services;

public class EpisodeApiService : ServiceBase, IEpisodeApiService
{
    public EpisodeApiService(
        IUnitOfWork unitOfWork, 
        IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<EpisodesResponseDto> GetEpisodesByShowIdAsync(int id)
    {
        var episodes = await UnitOfWork.EpisodeRepository.GetEpisodesByShowIdAsync(id);
        if (!episodes.Any())
            return new EpisodesResponseDto(
                string.Empty, 
                Severity.Info,
                HttpStatusCode.NoContent
            );
        
        var episodeModelList = Mapper.Map<ICollection<EpisodeModel>>(episodes);
        return new EpisodesResponseDto(
            string.Format(
                AppConstant.SuccessMessage.FetchedEpisodesSuccessfully,
                episodeModelList?.Count
            ),
            Severity.Success,
            HttpStatusCode.OK,
            episodeModelList
        );
    }
}