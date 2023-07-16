using TvMazeApp.Core.Enums;

namespace TvMazeApp.Core.Models;

public class ApiResponseBase
{
    public string? Message { get; set; }
    public Severity Severity { get; set; }

    public ApiResponseBase(
        string? message, 
        Severity severity)
    {
        Message = message;
        Severity = severity;
    }
}