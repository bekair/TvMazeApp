using TvMazeApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace TvMazeApp.DataAccess.Contexts;

public class TvMazeContext : DbContext
{
    public TvMazeContext(DbContextOptions<TvMazeContext> options) 
        : base(options)
    {
    }
    
    public DbSet<TvShow>? TvShows { get; set; }

    public DbSet<Episode>? Episodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region One-to-Many Relations

        //One-to-Many Relationship between Episode and TvShow
        modelBuilder.Entity<Episode>()
            .HasOne(x => x.TvShow)
            .WithMany(x => x.Episodes)
            .HasForeignKey(x => x.ShowId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}