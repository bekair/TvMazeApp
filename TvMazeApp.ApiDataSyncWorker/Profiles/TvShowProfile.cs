using AutoMapper;
using System.Collections.Generic;
using TvMazeApp.Entity;
using TvMazeApp.Scraper.BusinessLayer.Models;

namespace TvMazeApp.ApiDataSyncWorker.Profiles;

public class TvShowProfile : Profile
{
    public TvShowProfile()
    {
        CreateMap<TvShowModelWithEpisodes, TvShow>()
            .ForMember(
                dest => dest.ApiId,
                opts => opts.MapFrom(src => src.Id)
            ).ForPath(
                dest => dest.Episodes,
                opts => opts.MapFrom(src => src.EmbeddedEpisodeModel.Episodes)
            ).ForMember(
                dest => dest.Id,
                x => x.Ignore()
            );
        CreateMap<EpisodeModel, Episode>()
            .ForMember(
                dest => dest.ApiId,
                opts => opts.MapFrom(src => src.Id)
            ).ForMember(
                dest => dest.Id,
                x => x.Ignore()
            );
        CreateMap<TvShowModel, TvShow>()
            .ForMember(
                dest => dest.ApiId,
                opts => opts.MapFrom(src => src.Id)
            ).ForMember(
                dest => dest.Id,
                x => x.Ignore()
            );
    }
}