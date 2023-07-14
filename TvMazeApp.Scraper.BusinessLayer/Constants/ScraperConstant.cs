namespace TvMazeApp.Scraper.BusinessLayer.Constants;

public static class ScraperConstant
{
    public static class Uri
    {
        public const string SearchShowUri = "shows?q={0}";
        public const string ShowEpisodeListUri = "{0}?embed=episodes";
    }
    
    public static class ApiStatusResult
    {
        public const string Ended = "Ended";
        public const string NotFound = "404";
    }
}