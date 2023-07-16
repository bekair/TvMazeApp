using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Models;

namespace TvMazeApp.Scraper.BusinessLayer.Models.Responses;

public class TvShowAddResponse : ApiResponseBase
{
    public TvShowAddResponse( 
        string? message, 
        Severity severity) 
        : base(message, severity)
    {
    }
}