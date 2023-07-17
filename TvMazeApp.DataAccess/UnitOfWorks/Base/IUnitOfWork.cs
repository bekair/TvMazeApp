using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;

namespace TvMazeApp.DataAccess.UnitOfWorks.Base;

public interface IUnitOfWork : IDisposable
{
    ITvShowRepository TvShowRepository { get; }
    IEpisodeRepository EpisodeRepository { get; }
    Task CommitAsync();
}