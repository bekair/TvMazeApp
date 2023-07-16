using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses.Base;

namespace TvMazeApp.Scraper.BusinessLayer.Models;

public class TvShowModel : IApiStatus
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Summary { get; set; }
    
    public long Updated { get; set; }
    
    [JsonPropertyName(ScraperConstant.JsonPropertyName.TvShowLink)]
    public LinkModel? Links { get; set; }

    public string? Status { get; set; }
}