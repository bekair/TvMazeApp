using TvMazeApp.DataAccess.Contexts;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.DataAccess.UnitOfWorks.Base;

namespace TvMazeApp.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly TvMazeContext _context;
    public ITvShowRepository TvShowRepository { get; }

    public UnitOfWork(TvMazeContext context,
        ITvShowRepository tvShowRepository)
    {
        _context = context;
        TvShowRepository = tvShowRepository;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}