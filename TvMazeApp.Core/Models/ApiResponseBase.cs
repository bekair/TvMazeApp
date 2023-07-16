using System.Net;
using TvMazeApp.Core.Enums;

namespace TvMazeApp.Core.Models;

public class ApiResponseBase
{
    public string? Message { get; set; }
    public Severity Severity { get; set; }
    public HttpStatusCode? StatusCode { get; set; }

    public ApiResponseBase(
        string? message, 
        Severity severity, 
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Message = message;
        Severity = severity;
        StatusCode = statusCode;
    }
}