using System.Net;
using Microsoft.AspNetCore.Mvc;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;

namespace TvMazeApp.API.Controllers;

[Route("[controller]")]
[ApiController]
public class EpisodesController : ControllerBase
{
    private readonly IEpisodeApiService _episodeApiService;

    public EpisodesController(IEpisodeApiService episodeApiService)
    {
        _episodeApiService = episodeApiService ?? throw new ArgumentNullException(nameof(episodeApiService));
    }
    
    [HttpGet]
    [Route("GetEpisodesByShowId/{showId:int}")]
    public async Task<IActionResult> GetEpisodesByShowId(int showId)
    {
        if (showId <= 0)
            throw new ParameterException(AppConstant.ErrorMessage.IdLessThanOrEqualToZero);

        var returnedModel = await _episodeApiService.GetEpisodesByShowIdAsync(showId);
        
        return returnedModel.StatusCode == HttpStatusCode.OK 
            ? Ok(returnedModel)
            : NoContent();
    }
}