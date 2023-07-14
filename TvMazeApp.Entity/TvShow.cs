using TvMazeApp.Entity.Base;

namespace TvMazeApp.Entity;

public class TvShow : IEntity
{
    public int Id { get; set; }
    public int ApiId { get; set; }
    public long Updated { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    
    //Navigation Property
    public virtual IEnumerable<Episode>? Episodes { get; set; }
}