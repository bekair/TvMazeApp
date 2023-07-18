namespace TvMazeApp.Core.Constants;

public static class AppConstant
{
    public static class ErrorMessage
    {
        public const string DbDataNotFound = "The {0} could not be found with the requested id.";
        public const string ApiDataNotFound = "The data could not be found in the TvMaze API with the requested id.";
        public const string TvShowNameParameterNullOrEmpty = "Tv Show Name cannot be null or empty.";
        public const string UriParameterNullOrEmpty = "Uri cannot be null or empty.";
        public const string InternalServerError = "Something went wrong with the server. Please contact with the administrator.";
        public const string TvMazeSettingsNotFound = "Cannot find configuration section: {0}";
        public const string TvMazeConnectionStringNotFound = "TvMazeConnectionString could not be found.";
        public const string IdLessThanOrEqualToZero = "Id field cannot be less than or equal to '0'.";
        public const string NoTvShowExists = "No tv shows were found with the searched name.";
        public const string NoTvShowsExistedInDbWithTheApiId = "There is no Tv Show available to update with the given ApiId.";
        public const string NoTvShowsExistedInApiWithTheApiId = "There is no Tv Show available in the API with the given ApiId.";
    }
    
    public static class General
    {
        public const string ContentType = "application/json";
    }
    
    public static class InfoMessage
    {
        public const string AlreadyCreatedTvShows = "Tv Shows are already created.";
        public const string TvShowsAlreadySynced = "There is no change between the TvMaze API and the Data Storage";
    }
    
    public static class SuccessMessage
    {
        public const string SavedTvShowsSuccessfully = "TV Shows were saved successfully.";
        public const string SyncTvShowsAndEpisodesSuccessfully = "The TvShow and it's Episodes have been synchronized successfully.";
        public const string FetchedTvShowsSuccessfully = "{0} Tv Shows were fetched successfully.";
        public const string FetchedEpisodesSuccessfully = "{0} Episodes were fetched successfully.";
    }
}