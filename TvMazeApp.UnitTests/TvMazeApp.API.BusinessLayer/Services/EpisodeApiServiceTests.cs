using System.Net;
using AutoMapper;
using Moq;
using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.API.BusinessLayer.Services;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.Core.Enums;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.UnitTests.TvMazeApp.API.BusinessLayer.Mocks;

namespace TvMazeApp.UnitTests.TvMazeApp.API.BusinessLayer.Services;

[TestFixture]
public class EpisodeApiServiceTests
{
    private IEpisodeApiService? _episodeApiService;
    private Mock<IEpisodeRepository>? _mockEpisodeRepository;
    private Mock<IMapper>? _mockMapper;

    [SetUp]
    public void Setup()
    {
        _mockEpisodeRepository = new Mock<IEpisodeRepository>();
        _mockMapper = new Mock<IMapper>();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.EpisodeRepository)
            .Returns(_mockEpisodeRepository.Object);

        _episodeApiService = new EpisodeApiService(mockUnitOfWork.Object, _mockMapper.Object);
    }
    
    [Test]
    public async Task GetEpisodesByShowIdAsync_WithExistedShowId_ReturnsEpisodesResponseDto()
    {
        // Arrange
        const int testShowId = 1;
        var mockEpisodes = MockDataProvider
            .GetMockEpisodeList()
            .Where(e => e.ShowId == testShowId);
        
        _mockEpisodeRepository!
            .Setup(repo => repo
                .GetEpisodesByShowIdAsync(testShowId)
            ).ReturnsAsync(mockEpisodes);

        var mockEpisodeModels = MockDataProvider
            .GetMockEpisodeModelList()
            .Where(e => e.ShowId == testShowId);

        var mockMappedEpisodeModelList = MockDataProvider.MapMockEpisodeModelsToEpisodeModels(mockEpisodeModels);
        _mockMapper!
            .Setup(mapper => mapper
                .Map<ICollection<EpisodeModel>>(mockEpisodes))
            .Returns(mockMappedEpisodeModelList);

        // Act
        var result = await _episodeApiService!.GetEpisodesByShowIdAsync(testShowId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Severity, Is.EqualTo(Severity.Success.ToString()));
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Episodes, Is.EqualTo(mockMappedEpisodeModelList));
            Assert.That(string.IsNullOrWhiteSpace(result.Message), Is.False);
        });
    }
    
    [Test]
    public async Task GetEpisodesByShowIdAsync_WithShowId_WhenEpisodeListIsEmpty_ReturnsEpisodesResponseDtoWithInfo()
    {
        // Arrange
        const int testShowId = 100000;
        var mockEpisodeList = MockDataProvider
            .GetMockEpisodeList()
            .Where(e => e.ShowId == testShowId);
        
        _mockEpisodeRepository!
            .Setup(repo => repo
                .GetEpisodesByShowIdAsync(testShowId)
            ).ReturnsAsync(mockEpisodeList);

        // Act
        var result = await _episodeApiService!.GetEpisodesByShowIdAsync(testShowId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Severity, Is.EqualTo(Severity.Info.ToString()));
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(result.Message, Is.EqualTo(string.Empty));
        });
    }
}