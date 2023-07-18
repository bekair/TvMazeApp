using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Entity;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Models;
using System.Net.Http;
using System.Net;

namespace TvMazeApp.ApiDataSyncWorker
{
    public class TvMazeDataSync
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiCaller _apiCaller;
        private readonly IMapper _mapper;

        public TvMazeDataSync(
            IUnitOfWork unitOfWork, 
            IApiCaller apiCaller, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _apiCaller = apiCaller;
            _mapper = mapper;
        }

        [FunctionName("TvMazeDataSync")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            var tvShowApiId = Convert.ToInt32(req.Query["tvShowApiId"]);

            TvShowModelWithEpisodes apiTvShowWithEpisodes;
            try
            {
                apiTvShowWithEpisodes = await _apiCaller.GetTvShowWithEpisodesByApiId(tvShowApiId);
                if (apiTvShowWithEpisodes is null)
                    throw new DataNotFoundException(AppConstant.ErrorMessage.ApiDataNotFound);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return new OkObjectResult(AppConstant.ErrorMessage.NoTvShowExists);

                throw ex;
            }
            catch (DataNotFoundException)
            {
                return new OkObjectResult(AppConstant.ErrorMessage.NoTvShowExists);
            }

            TvShow notUpdatedTvShow;
            try
            {
                notUpdatedTvShow = await _unitOfWork.TvShowRepository.GetIncludeByAsync(s =>
                        s.ApiId == tvShowApiId &&
                        s.Updated != apiTvShowWithEpisodes.Updated,
                    s => s.Episodes
                );
            }
            catch (DataNotFoundException)
            {
                return new OkObjectResult(AppConstant.InfoMessage.TvShowsAlreadySynced);
            }

            notUpdatedTvShow = _mapper.Map(apiTvShowWithEpisodes, notUpdatedTvShow);
            _unitOfWork.TvShowRepository.Update(notUpdatedTvShow);
            await _unitOfWork.CommitAsync();
            
            return new OkObjectResult(AppConstant.SuccessMessage.SyncTvShowsAndEpisodesSuccessfully);
        }
    }
}
