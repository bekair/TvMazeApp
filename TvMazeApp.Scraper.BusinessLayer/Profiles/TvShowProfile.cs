using AutoMapper;
using TvMazeApp.Entity;
using TvMazeApp.Scraper.BusinessLayer.Models;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses;

namespace TvMazeApp.Scraper.BusinessLayer.Profiles;

public class TvShowProfile : Profile
{
    public TvShowProfile()
    {
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