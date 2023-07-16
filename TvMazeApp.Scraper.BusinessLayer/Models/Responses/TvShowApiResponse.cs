using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Constants;

namespace TvMazeApp.Scraper.BusinessLayer.Models.Responses;

public class TvShowApiResponse
{
    [JsonPropertyName(ScraperConstant.JsonPropertyName.Show)]
    public TvShowModel? TvShow { get; set; }
}