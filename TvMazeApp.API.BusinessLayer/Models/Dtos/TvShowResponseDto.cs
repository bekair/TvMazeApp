using System.Net;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Models;

namespace TvMazeApp.API.BusinessLayer.Models.Dtos;

public class TvShowResponseDto : ApiResponseBase
{
    public new string Severity { get; set; }

    public TvShowResponseDto(
        string? message, 
        Severity severity,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        ICollection<TvShowModel>? tvShows = null) : base(message, severity, statusCode)
    {
        Severity = severity.ToString();
        TvShows = tvShows;
    }

    public ICollection<TvShowModel>? TvShows { get; set; }
}