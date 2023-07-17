using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Moq;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Entity;
using TvMazeApp.Scraper.BusinessLayer.ApiCaller.Interfaces;
using TvMazeApp.Scraper.BusinessLayer.Models;
using TvMazeApp.Scraper.BusinessLayer.Models.Responses;
using TvMazeApp.Scraper.BusinessLayer.Services;
using TvMazeApp.UnitTests.TvMazeApp.Scraper.BusinessLayer.Mocks;

namespace TvMazeApp.UnitTests.TvMazeApp.Scraper.BusinessLayer.Services;

[TestFixture]
public class TvShowServiceTests
{
    private TvShowService? _tvShowService;
    private Mock<IApiCaller>? _mockApiCaller;
    private Mock<IUnitOfWork>? _mockUnitOfWork;
    private Mock<IMapper>? _mockMapper;

    [SetUp]
    public void Setup()
    {
        _mockApiCaller = new Mock<IApiCaller>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();

        _tvShowService = new TvShowService(
            _mockApiCaller.Object,
            _mockUnitOfWork.Object,
            _mockMapper.Object
        );
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void AddShowsByNameWithEpisodesAsync_WhenEmptyNullOrWhitespaceShowName_ThrowsParameterException(string showName)
    {
        //Act
        async Task ExecutedTask() => await _tvShowService!.AddShowsByNameWithEpisodesAsync(showName);

        // Assert
        Assert.ThrowsAsync<ParameterException>(ExecutedTask);
    }

    [Test]
    public async Task AddShowsByNameWithEpisodesAsync_WhenNoShowsExisted_ReturnsTvShowAddResponseWithInfo()
    {
        // Arrange
        const string showName = "Any";
        _mockApiCaller!.Setup(apiCaller => apiCaller
            .GetTvShowsByNameAsync(It.IsAny<string>())
        ).ReturnsAsync(new List<TvShowApiResponse>());

        var idList = new List<int> { 1 };
        _mockUnitOfWork!.Setup(unitOfWork => unitOfWork.TvShowRepository
            .GetAsync(It.IsAny<Expression<Func<TvShow, bool>>>())
        ).ReturnsAsync(idList.Select(id => new TvShow { ApiId = id }));

        // Act
        var result = await _tvShowService!.AddShowsByNameWithEpisodesAsync(showName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Severity, Is.EqualTo(Severity.Info));
            Assert.That(string.IsNullOrWhiteSpace(result.Message), Is.False);
        });
    }

    [Test]
    public async Task AddShowsByNameWithEpisodesAsync_ShowsWithEpisodesSave_ReturnsTvShowAddResponseWithSuccess()
    {
        // Arrange
        const string showName = "show name";
        var mockTvShowsApiResponse = MockDataProvider.GetMockTvShowsApiResponse();
        var mockShowEpisodesApiResponse = MockDataProvider.GetMockShowEpisodesApiResponse();
        _mockApiCaller!.Setup(caller => caller
            .GetTvShowsByNameAsync(It.IsAny<string>())
        ).ReturnsAsync(mockTvShowsApiResponse);

        _mockApiCaller.Setup(apiCaller => apiCaller
            .GetTvShowsWithEpisodesByShowId(It.IsAny<string>())
        ).ReturnsAsync(mockShowEpisodesApiResponse);

        var mockMappedTvShow = new TvShow();
        _mockMapper!.Setup(mapper => mapper
            .Map<TvShow>(It.IsAny<TvShowModel>())
        ).Returns(mockMappedTvShow);
        
        _mockMapper!.Setup(mapper => mapper
            .Map<IEnumerable<Episode>>(mockShowEpisodesApiResponse)
        ).Returns(It.IsAny<IEnumerable<Episode>>());
        
        _mockUnitOfWork!.Setup(unitOfWork => unitOfWork.TvShowRepository.AddTvShowWithEpisodes(It.IsAny<TvShow>()));
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.CommitAsync());

        // Act
        var result = await _tvShowService!.AddShowsByNameWithEpisodesAsync(showName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Severity, Is.EqualTo(Severity.Success));
            Assert.That(string.IsNullOrWhiteSpace(result.Message), Is.False);
        });

        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.TvShowRepository.AddTvShowWithEpisodes(mockMappedTvShow), Times.Exactly(mockTvShowsApiResponse.Count));
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.CommitAsync(), Times.Once);
    }
}