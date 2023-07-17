using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Base;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.Entity;

namespace TvMazeApp.DataAccess.Repositories.Implementations;

public class EpisodeRepository : RepositoryBase<Episode,TvMazeContext>, IEpisodeRepository
{
    public EpisodeRepository(TvMazeContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<Episode>> GetEpisodesByShowIdAsync(int showId)
    {
        return await GetAsync(e => e.ShowId == showId);
    }
}