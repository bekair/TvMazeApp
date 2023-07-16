using System.Net;
using System.Text.Json;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.Core.Models;

namespace TvMazeApp.API.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = AppConstant.General.ContentType;

            var apiResponse = new ApiResponseBase
            (
                message: exception switch
                {
                    DataNotFoundException or
                        ParameterException => exception.Message,
                    _ => AppConstant.ErrorMessage.InternalServerError
                },
                severity: Severity.Error,
                statusCode: HttpStatusCode.InternalServerError
            );

            response.StatusCode = StatusCodes.Status500InternalServerError;

            await response.WriteAsync(JsonSerializer.Serialize(apiResponse));
        }
    }
}