using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses.Base;

namespace TvMazeApp.Scraper.BusinessLayer.Models;

public class TvShowModel : IApiStatus
{
    public int Id { get; set; }
    
    [JsonPropertyName("_links.self.href")]
    public string? Href { get; set; }

    public string? Status { get; set; }
}