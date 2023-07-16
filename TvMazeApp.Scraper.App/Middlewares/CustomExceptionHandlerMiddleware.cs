using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.Scraper.App.Models;

namespace TvMazeApp.Scraper.App.Middlewares;

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
            
            var errorModel = new ErrorModel
            {
                Message = exception switch
                {
                    DataNotFoundException or
                        ParameterException => exception.Message,
                    _ => AppConstant.ErrorMessage.InternalServerError
                }
            };
            response.StatusCode = StatusCodes.Status500InternalServerError;

            await response.WriteAsync(errorModel.ToString());
        }
    }
}