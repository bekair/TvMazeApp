using TvMazeApp.API.BusinessLayer.Models.Dtos;

namespace TvMazeApp.API.BusinessLayer.Services.Interfaces;

public interface IEpisodeApiService
{
    Task<EpisodesResponseDto> GetEpisodesByShowIdAsync(int id);
}