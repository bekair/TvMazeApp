using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Base;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.Entity;

namespace TvMazeApp.DataAccess.Repositories.Implementations;

public class TvShowRepository : RepositoryBase<TvShow,TvMazeContext>, ITvShowRepository
{
    public TvShowRepository(TvMazeContext context) 
        : base(context)
    {
    }

    public void AddTvShowWithEpisodes(TvShow tvShow)
    {
        Insert(tvShow);
    }
}