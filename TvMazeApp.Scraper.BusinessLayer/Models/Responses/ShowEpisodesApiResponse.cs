using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses.Base;

namespace TvMazeApp.Scraper.BusinessLayer.Models.Responses;

public class ShowEpisodesApiResponse : IApiStatus
{
    [JsonPropertyName(ScraperConstant.JsonPropertyName.EpisodeEmbeddedList)]
    public EmbeddedEpisodeModel? EmbeddedEpisodeModel { get; set; }

    public string? Status { get; set; }
}