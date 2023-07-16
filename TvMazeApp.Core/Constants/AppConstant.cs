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
    }
    
    public static class General
    {
        public const string ContentType = "application/json";
    }
}