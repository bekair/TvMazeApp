using System.Text.Json;

namespace TvMazeApp.Scraper.BusinessLayer.Helpers;

public static class SerializerHelper
{
    public static async Task<TResponse?> DeserializeAsync<TResponse>(HttpResponseMessage httpResponseMessage)
    {
        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        return await JsonSerializer.DeserializeAsync<TResponse>(stream, serializeOptions);
    }
}