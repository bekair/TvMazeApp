using System.Text.Json;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Constants;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses;

namespace TvMazeApp.Scraper.BusinessLayer.ApiCaller;

public class ApiCaller : IApiCaller
{
    private readonly HttpClient _httpClient;
    
    public ApiCaller(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<ICollection<TvShowApiResponse>> GetTvShowsByNameAsync(string uri)
    {
        ValidateUri(uri);
        
        var response = (await _httpClient.GetAsync(uri)).EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var deserializedShow = await JsonSerializer.DeserializeAsync<ICollection<TvShowApiResponse>>(stream);
        
        if (deserializedShow is null || !deserializedShow.Any())
            throw new DataNotFoundException(AppConstant.ErrorMessage.NoTvShowExists);

        return deserializedShow;
    }

    public async Task<ShowEpisodesApiResponse?> GetTvShowsWithEpisodesByShowId(string uri)
    {
        ValidateUri(uri);
        
        var response = (await _httpClient.GetAsync(uri)).EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var deserializedShowWithEpisodes = await JsonSerializer.DeserializeAsync<ShowEpisodesApiResponse>(stream);
        
        if (deserializedShowWithEpisodes is null || 
            deserializedShowWithEpisodes.Status == ScraperConstant.ApiStatusResult.NotFound)
            throw new DataNotFoundException(AppConstant.ErrorMessage.NoTvShowExists);
        
        return deserializedShowWithEpisodes;
    }

    private static void ValidateUri(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
            throw new ArgumentException(AppConstant.ErrorMessage.UriParameterNullOrEmpty);
    }
}