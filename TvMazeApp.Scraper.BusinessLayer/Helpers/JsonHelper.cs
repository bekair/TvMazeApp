using System.Text.Json;

namespace TvMazeApp.Scraper.BusinessLayer.Helpers;

public static class JsonHelper
{
    public static TReturn? DeserializeResponse<TReturn>(string json, JsonSerializerOptions? options = null)
    {
        var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("value", out var valueElement)) 
            return default!;

        var value = JsonSerializer.Serialize(valueElement, options);
        return JsonSerializer.Deserialize<TReturn>(value, options);
    }
}