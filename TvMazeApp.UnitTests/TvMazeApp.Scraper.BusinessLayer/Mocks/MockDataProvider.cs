using TvMazeApp.Scraper.BusinessLayer.Models;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses;

namespace TvMazeApp.UnitTests.TvMazeApp.Scraper.BusinessLayer.Mocks;

public static class MockDataProvider
{
    public static ICollection<TvShowApiResponse> GetMockTvShowsApiResponse()
    {
        return new List<TvShowApiResponse>
        {
            new()
            {
                TvShow = new TvShowModel
                {
                    Id = 868, Name = "Regular Show", Status = "Ended",
                    Summary = "<p>The<b> Regular Show</b> is about Mordecai and Rigby.</p>",
                    Updated = 1615068425
                }
            },
            new()
            {
                TvShow = new TvShowModel
                {
                    Id = 4128, Name = "Chappelle's Show", Status = "Ended",
                    Summary = "<p>It's not just a show -- it's a social phenomenon.",
                    Updated = 1623008840
                }
            }
        };
    }

    public static ShowEpisodesApiResponse GetMockShowEpisodesApiResponse()
    {
        return new ShowEpisodesApiResponse
        {
            Status = "Ended",
            EmbeddedEpisodeModel = new EmbeddedEpisodeModel
            {
                Episodes = GetMockEpisodeModelList()
            }
        };
    }

    public static ICollection<EpisodeModel> GetMockEpisodeModelList()
    {
        var mockEpisodeModelList = new List<EpisodeModel>
        {
            new()
            {
                ShowId = 1, Name = "The Power", 
                Summary = "<p>The Power Summary</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 1, Name = "Just Set Up The Chairs", 
                Summary = "",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                ShowId = 1, Name = "Caffeinated Concert Tickets", 
                Summary = "<p>The<b> Regular Show</b> is about Mordecai and Rigby</p>",
                SeasonNumber = 1, EpisodeNumber = 3
            },
            new()
            {
                ShowId = 2, Name = "Black White Supremacist", 
                Summary = "<p>Dave pokes fun at an annoying Mitsubishi car commercial.</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 2, Name = "Introducing Tyrone Biggums", 
                Summary = "<p>Dave plays crack addict Tyrone Biggums.</p>",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                ShowId = 2, Name = "The Racial Draft", 
                Summary = "<p>Dave runs for office and offers a look at life in slow motion.</p>",
                SeasonNumber = 2, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 4, Name = "Warring Factions", 
                Summary = "<p>Despite being bullied on the street, Mark sets himself the task.</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 4, Name = "The Interview", 
                Summary = "<p>Jeremy and Super Hans' band, the Hair Blair Bunch, is going nowhere.</p>",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                ShowId = 4, Name = "Local Zero", 
                Summary = "<p>Jeff is going on a work trip with Sophie to Aberdeen.</p>",
                SeasonNumber = 2, EpisodeNumber = 3
            }
        };

        return mockEpisodeModelList;
    }
}