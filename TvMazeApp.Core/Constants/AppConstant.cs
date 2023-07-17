namespace TvMazeApp.Core.Constants;

public static class AppConstant
{
    public static class ErrorMessage
    {
        public const string DbDataNotFound = "The {0} could not be found with the requested id.";
        public const string NoTvShowExists = "No tv shows were found with the searched name.";
        public const string TvShowNameParameterNullOrEmpty = "Tv Show Name cannot be null or empty.";
        public const string UriParameterNullOrEmpty = "Uri cannot be null or empty.";
        public const string InternalServerError = "Something went wrong with the server. Please contact with the administrator.";
        public const string TvMazeSettingsNotFound = "Cannot find configuration section: {0}";
        public const string TvMazeConnectionStringNotFound = "TvMazeConnectionString could not be found.";
        public const string IdLessThanOrEqualToZero = "Id field cannot be less than or equal to '0'.";
    }
    
    public static class General
    {
        public const string ContentType = "application/json";
    }
    
    public static class InfoMessage
    {
        public const string AlreadyCreatedTvShows = "Tv Shows are already created.";
    }
    
    public static class SuccessMessage
    {
        public const string SavedTvShowsSuccessfully = "TV Shows were saved successfully.";
        public const string FetchedTvShowsSuccessfully = "{0} Tv Shows were fetched successfully.";
        public const string FetchedEpisodesSuccessfully = "{0} Episodes were fetched successfully.";
    }
}