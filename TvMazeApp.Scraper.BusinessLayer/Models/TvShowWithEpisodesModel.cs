using System.Text.Json.Serialization;
using TvMazeApp.Scraper.BusinessLayer.Constants;

namespace TvMazeApp.Scraper.BusinessLayer.Models
{
    public class TvShowModelWithEpisodes : TvShowModel
    {
        [JsonPropertyName(ScraperConstant.JsonPropertyName.EpisodeEmbeddedList)]
        public EmbeddedEpisodeModel? EmbeddedEpisodeModel { get; set; }
    }
}
