using System.Net;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Models;

namespace TvMazeApp.API.BusinessLayer.Models.Dtos;

public class EpisodesResponseDto : ApiResponseBase
{
    public new string Severity { get; set; }
    
    public EpisodesResponseDto(
        string? message, 
        Severity severity, 
        HttpStatusCode statusCode = HttpStatusCode.OK,
        ICollection<EpisodeModel>? episodes = null) : base(message, severity, statusCode)
    {
        Severity = severity.ToString();
        Episodes = episodes;
    }
    
    public ICollection<EpisodeModel>? Episodes { get; set; }
}