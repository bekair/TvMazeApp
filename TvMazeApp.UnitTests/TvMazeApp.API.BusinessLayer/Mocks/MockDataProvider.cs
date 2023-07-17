using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.Entity;
using TvMazeApp.UnitTests.TvMazeApp.API.BusinessLayer.Mocks.Models;

namespace TvMazeApp.UnitTests.TvMazeApp.API.BusinessLayer.Mocks;

public static class MockDataProvider
{
    public static ICollection<TvShow> GetMockTvShowList()
    {
        var mockTvShows = new List<TvShow>
        {
            new()
            {
                Id = 1, ApiId = 868, Name = "Regular Show", 
                Updated = 1615068425, Summary = "<p>The<b> Regular Show</b> is about Mordecai and Rigby</p>",
                Episodes = GetMockEpisodeList().Where(x => x.ShowId == 1)
            },
            new()
            {
                Id = 2, ApiId = 4128, Name = "Chappelle's Show", 
                Updated = 1623008840, Summary = "<p>It's not just a show -- it's a social phenomenon.</p>",
                Episodes = GetMockEpisodeList().Where(x => x.ShowId == 2)
            },
            new()
            {
                Id = 4, ApiId = 383, Name = "Peep Show", 
                Updated = 1685113890, Summary = "<p>Award-winning sitcom.</p>",
                Episodes = GetMockEpisodeList().Where(x => x.ShowId == 4)
            }
        };

        return mockTvShows;
    }

    public static ICollection<TvShowModel> GetMockTvShowModelList()
    {
        var mockedTvShowModelList = new List<TvShowModel>
        {
            new()
            {
                Id = 1, 
                Title = "Regular Show", 
                Summary = "<p>The<b> Regular Show</b> is about Mordecai and Rigby</p>"
            },
            new()
            {
                Id = 2, 
                Title = "Chappelle's Show",
                Summary = "<p>It's not just a show -- it's a social phenomenon.</p>"
            },
            new()
            {
                Id = 4,
                Title = "Peep Show", 
                Summary = "<p>Award-winning sitcom.</p>",
            }
        };

        return mockedTvShowModelList;
    }

    public static ICollection<Episode> GetMockEpisodeList()
    {
        var mockEpisodes = new List<Episode>
        {
            new()
            {
                Id = 1, ApiId = 80586, ShowId = 1, 
                Name = "The Power", 
                Summary = "<p>The Power Summary</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                Id = 1, ApiId = 80587, ShowId = 1, 
                Name = "Just Set Up The Chairs", 
                Summary = "",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                Id = 1, ApiId = 80588, ShowId = 1, 
                Name = "Caffeinated Concert Tickets", 
                Summary = "<p>The<b> Regular Show</b> is about Mordecai and Rigby</p>",
                SeasonNumber = 1, EpisodeNumber = 3
            },
            new()
            {
                Id = 247, ApiId = 263167, ShowId = 2, 
                Name = "Black White Supremacist", 
                Summary = "<p>Dave pokes fun at an annoying Mitsubishi car commercial.</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                Id = 248, ApiId = 263168, ShowId = 2, 
                Name = "Introducing Tyrone Biggums", 
                Summary = "<p>Dave plays crack addict Tyrone Biggums.</p>",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                Id = 259, ApiId = 263181, ShowId = 2, 
                Name = "The Racial Draft", 
                Summary = "<p>Dave runs for office and offers a look at life in slow motion.</p>",
                SeasonNumber = 2, EpisodeNumber = 1
            },
            new()
            {
                Id = 714, ApiId = 35097, ShowId = 4, 
                Name = "Warring Factions", 
                Summary = "<p>Despite being bullied on the street, Mark sets himself the task.</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                Id = 715, ApiId = 35098, ShowId = 4, 
                Name = "The Interview", 
                Summary = "<p>Jeremy and Super Hans' band, the Hair Blair Bunch, is going nowhere.</p>",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                Id = 722, ApiId = 35105, ShowId = 4, 
                Name = "Local Zero", 
                Summary = "<p>Jeff is going on a work trip with Sophie to Aberdeen.</p>",
                SeasonNumber = 2, EpisodeNumber = 3
            }
        };

        return mockEpisodes;
    }
    
    public static ICollection<MockEpisodeModel> GetMockEpisodeModelList()
    {
        var mockEpisodeModelList = new List<MockEpisodeModel>
        {
            new()
            {
                ShowId = 1, Title = "The Power", 
                Summary = "<p>The Power Summary</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 1, Title = "Just Set Up The Chairs", 
                Summary = "",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                ShowId = 1, Title = "Caffeinated Concert Tickets", 
                Summary = "<p>The<b> Regular Show</b> is about Mordecai and Rigby</p>",
                SeasonNumber = 1, EpisodeNumber = 3
            },
            new()
            {
                ShowId = 2, Title = "Black White Supremacist", 
                Summary = "<p>Dave pokes fun at an annoying Mitsubishi car commercial.</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 2, Title = "Introducing Tyrone Biggums", 
                Summary = "<p>Dave plays crack addict Tyrone Biggums.</p>",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                ShowId = 2, Title = "The Racial Draft", 
                Summary = "<p>Dave runs for office and offers a look at life in slow motion.</p>",
                SeasonNumber = 2, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 4, Title = "Warring Factions", 
                Summary = "<p>Despite being bullied on the street, Mark sets himself the task.</p>",
                SeasonNumber = 1, EpisodeNumber = 1
            },
            new()
            {
                ShowId = 4, Title = "The Interview", 
                Summary = "<p>Jeremy and Super Hans' band, the Hair Blair Bunch, is going nowhere.</p>",
                SeasonNumber = 1, EpisodeNumber = 2
            },
            new()
            {
                ShowId = 4, Title = "Local Zero", 
                Summary = "<p>Jeff is going on a work trip with Sophie to Aberdeen.</p>",
                SeasonNumber = 2, EpisodeNumber = 3
            }
        };

        return mockEpisodeModelList;
    }

    public static ICollection<EpisodeModel> MapMockEpisodeModelsToEpisodeModels(IEnumerable<MockEpisodeModel> mockEpisodeModelList)
    {
        return mockEpisodeModelList.Select(mockEpisodeModel => new EpisodeModel
        {
            SeasonNumber = mockEpisodeModel.SeasonNumber,
            EpisodeNumber = mockEpisodeModel.EpisodeNumber,
            Title = mockEpisodeModel.Title,
            Summary = mockEpisodeModel.Summary
        }).ToList();
    }
}