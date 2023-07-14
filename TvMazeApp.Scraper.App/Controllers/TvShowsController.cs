using Microsoft.AspNetCore.Mvc;
using TvMazeApp.Core.Constants;
using TvMazeApp.Scraper.BusinessLayer.Services.Interfaces;

namespace TvMazeApp.Scraper.App.Controllers
{
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
        public IActionResult AddTvShowsByNameWithEpisodes(string showName)
        {
            if(string.IsNullOrWhiteSpace(showName))
                throw new ArgumentException(AppConstant.ErrorMessage.TvShowNameParameterNullOrEmpty);
                    
            return Ok(_tvShowService.AddShowsByNameWithEpisodesAsync(showName));
        }
    }
}