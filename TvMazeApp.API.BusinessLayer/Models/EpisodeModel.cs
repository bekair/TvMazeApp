namespace TvMazeApp.API.BusinessLayer.Models;

public class EpisodeModel
{
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
}