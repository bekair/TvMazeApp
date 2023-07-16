using System.Net;
using AutoMapper;
using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.API.BusinessLayer.Models.Dtos;
using TvMazeApp.API.BusinessLayer.Services.Base;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.DataAccess.UnitOfWorks.Base;

namespace TvMazeApp.API.BusinessLayer.Services;

public class TvShowsApiService : ServiceBase, ITvShowsApiService
{
    public TvShowsApiService(IUnitOfWork unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper)
    {
    }
    
    public async Task<TvShowResponseDto> GetTvShowsByPartialNameAsync(string? showName)
    {
        if (string.IsNullOrWhiteSpace(showName))
            throw new ParameterException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);
        
        var tvShows = await UnitOfWork.TvShowRepository.GetTvShowByPartialNameAsync(showName);
        if (!tvShows.Any())
            return new TvShowResponseDto(
                AppConstant.InfoMessage.NoTvShowsWithSearchedName,
                Severity.Info,
                HttpStatusCode.NoContent
            );

        var tvShowsModel = Mapper.Map<ICollection<TvShowModel>>(tvShows);
        return new TvShowResponseDto(
            string.Format(
                AppConstant.SuccessMessage.FetchedTvShowsSuccessfully,
                tvShowsModel.Count
            ),
            Severity.Success,
            HttpStatusCode.OK,
            tvShowsModel
        );
    }
}