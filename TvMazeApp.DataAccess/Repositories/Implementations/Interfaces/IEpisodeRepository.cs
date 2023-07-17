using TvMazeApp.Entity;

namespace TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;

public interface IEpisodeRepository
{
    Task<IEnumerable<Episode>> GetEpisodesByShowIdAsync(int showId);
}