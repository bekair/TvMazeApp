using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses.Base;

namespace TvMazeApp.Scraper.BusinessLayer.Models.Responses;

public class ShowEpisodesApiResponse : IApiStatus
{
    [JsonPropertyName("_embedded.episodes")]
    public ICollection<EpisodeModel>? Episodes { get; set; }

    public string? Status { get; set; }
}