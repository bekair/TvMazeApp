using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses.Base;

namespace TvMazeApp.Scraper.BusinessLayer.Models;

public class EpisodeModel : IApiStatus
{
    public int Id { get; set; }
    
    public int ShowId { get; set; }
    
    public long Updated { get; set; }
    
    [JsonPropertyName("Season")]
    public int SeasonNumber { get; set; }
    
    [JsonPropertyName("Number")]
    public int EpisodeNumber { get; set; }
    
    public string? Title { get; set; }
    
    public string? Summary { get; set; }
    
    public string? Status { get; set; }
}