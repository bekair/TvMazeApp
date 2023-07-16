using System.Text.Json.Serialization;
using TvMazeApp.Entity.Base;

namespace TvMazeApp.Entity;

public class Episode : IEntity
{
    public int Id { get; set; }
    public int ApiId { get; set; }
    public int ShowId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    
    //Navigation Property
    public virtual TvShow? TvShow { get; set; }
}