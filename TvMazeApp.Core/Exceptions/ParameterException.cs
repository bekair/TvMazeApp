namespace TvMazeApp.Core.Exceptions;

public class ParameterException : ArgumentException
{
    public ParameterException(string message)
        : base(message)
    {
    }
}