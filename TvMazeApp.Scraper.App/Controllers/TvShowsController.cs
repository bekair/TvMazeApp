using Microsoft.AspNetCore.Mvc;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

namespace TvMazeApp.Scraper.App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        private readonly ILogger<TvShowsController> _logger;
        private readonly ITvShowService _tvShowService;

        public TvShowsController(
            ILogger<TvShowsController> logger,
            ITvShowService tvShowService)
        {
            _logger = logger;
            _tvShowService = tvShowService;
        }
        
        [HttpPost]
        [Route("AddTvShowsByNameWithEpisodes/{showName}")]
        public async Task<IActionResult> AddTvShowsByNameWithEpisodes(string showName)
        {
            if (string.IsNullOrWhiteSpace(showName))
                throw new ParameterException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);

            await _tvShowService.AddShowsByNameWithEpisodesAsync(showName);
            
            return Ok();
        }
    }
}