using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses.Base;

namespace TvMazeApp.Scraper.BusinessLayer.Models;

public class EpisodeModel : IApiStatus
{
    public int Id { get; set; }
    
    public int ShowId { get; set; }
    
    [JsonPropertyName(ScraperConstant.JsonPropertyName.Season)]
    public int SeasonNumber { get; set; }
    
    [JsonPropertyName(ScraperConstant.JsonPropertyName.EpisodeNumber)]
    public int EpisodeNumber { get; set; }
    
    public string? Name { get; set; }
    
    public string? Summary { get; set; }
    
    public string? Status { get; set; }
}