using TvMazeApp.API.BusinessLayer.Models.Dtos;

namespace TvMazeApp.API.BusinessLayer.Services.Interfaces;

public interface ITvShowsApiService
{
    Task<TvShowResponseDto> GetTvShowsByPartialNameAsync(string? showName);
}