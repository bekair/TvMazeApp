using AutoMapper;
using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.Entity;

namespace TvMazeApp.API.BusinessLayer.Profiles;

public class TvShowProfile : Profile
{
    public TvShowProfile()
    {
        CreateMap<TvShow, TvShowModel>()
            .ForMember(
                dest => dest.Title,
                opts => opts.MapFrom(src => src.Name)
            );
    }
}