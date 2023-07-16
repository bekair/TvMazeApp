namespace TvMazeApp.Scraper.BusinessLayer.Constants;

public static class ScraperConstant
{
    public static class Uri
    {
        public const string SearchShowUri = "search/shows?q={0}";
        public const string ShowEpisodeListUri = "{0}?embed=episodes";
    }
    
    public static class ApiStatusResult
    {
        public const string NotFound = "404";
    }
    
    public static class JsonPropertyName
    {
        public const string Season = "season";
        public const string EpisodeNumber = "number";
        public const string Show = "show";
        public const string EpisodeEmbeddedList = "_embedded";
        public const string TvShowLink = "_links";
    }
}