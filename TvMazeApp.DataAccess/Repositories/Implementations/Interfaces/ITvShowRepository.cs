using TvMazeApp.DataAccess.Repositories.Base;
using TvMazeApp.Entity;

namespace TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;

public interface ITvShowRepository : IRepositoryBase<TvShow>
{
    void AddTvShowWithEpisodes(TvShow tvShow);
}