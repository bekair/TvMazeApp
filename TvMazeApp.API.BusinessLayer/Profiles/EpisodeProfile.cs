using AutoMapper;
using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.Entity;

namespace TvMazeApp.API.BusinessLayer.Profiles;

public class EpisodeProfile : Profile
{
    public EpisodeProfile()
    {
        CreateMap<Episode, EpisodeModel>()
            .ForMember(
                dest => dest.Title,
                opts => opts.MapFrom(src => src.Name)
            );
    }
}