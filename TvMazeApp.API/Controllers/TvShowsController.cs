using System.Net;
using Microsoft.AspNetCore.Mvc;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;

namespace TvMazeApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TvShowsController : ControllerBase
{
    private readonly ITvShowsApiService _tvShowsApiService;

    public TvShowsController(ITvShowsApiService tvShowsApiService)
    {
        _tvShowsApiService = tvShowsApiService ?? throw new ArgumentNullException(nameof(tvShowsApiService));
    }
        
    [HttpGet]
    [Route("{showName}")]
    public async Task<IActionResult> SearchTvShowsByName(string showName)
    {
        if (string.IsNullOrWhiteSpace(showName))
            throw new ParameterException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);

        var returnedModel = await _tvShowsApiService.GetTvShowsByPartialNameAsync(showName);
        
        return returnedModel.StatusCode == HttpStatusCode.OK 
            ? Ok(returnedModel)
            : NoContent();
    }
}