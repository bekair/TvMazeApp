using System.Text.Json;

namespace TvMazeApp.Scraper.App.Models;

public class ErrorModel
{
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}