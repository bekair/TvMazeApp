using System.Net;
using AutoMapper;
using Moq;
using TvMazeApp.API.BusinessLayer.Models;
using TvMazeApp.API.BusinessLayer.Services;
using TvMazeApp.API.BusinessLayer.Services.Interfaces;
using TvMazeApp.Core.Enums;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.DataAccess.Repositories.Implementations.Interfaces;
using TvMazeApp.DataAccess.UnitOfWorks.Base;
using TvMazeApp.Entity;
using TvMazeApp.UnitTests.TvMazeApp.API.BusinessLayer.Mocks;

namespace TvMazeApp.UnitTests.TvMazeApp.API.BusinessLayer.Services;

[TestFixture]
public class TvShowServiceTests
{
    private ITvShowsApiService? _tvShowsApiService;
    private Mock<ITvShowRepository>? _mockTvShowRepository;
    private Mock<IMapper>? _mockMapper;

    [SetUp]
    public void Setup()
    {
        _mockTvShowRepository = new Mock<ITvShowRepository>();
        _mockMapper = new Mock<IMapper>();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.TvShowRepository)
            .Returns(_mockTvShowRepository.Object);

        _tvShowsApiService = new TvShowsApiService(mockUnitOfWork.Object, _mockMapper.Object);
    }
    
    [Test]
    public async Task GetTvShowsByPartialNameAsync_WithValidName_ReturnsTvShowResponseDto()
    {
        // Arrange
        var mockedTvShows = MockDataProvider.GetMockTvShowList();
        const string enteredShowName = "Any";
        
        _mockTvShowRepository!
            .Setup(repo => repo
                .GetTvShowByPartialNameAsync(It.IsAny<string>())
            ).ReturnsAsync(mockedTvShows);

        var mockedTvShowModels = MockDataProvider.GetMockTvShowModelList();
            
        _mockMapper!
            .Setup(mapper => mapper
                .Map<ICollection<TvShowModel>>(mockedTvShows))
            .Returns(mockedTvShowModels);

        // Act
        var result = await _tvShowsApiService!.GetTvShowsByPartialNameAsync(enteredShowName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Severity, Is.EqualTo(Severity.Success.ToString()));
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.TvShows, Is.EqualTo(mockedTvShowModels));
            Assert.That(string.IsNullOrWhiteSpace(result.Message), Is.False);
        });
    }
    
    [Test]
    public async Task GetTvShowsByPartialNameAsync_WithValidName_WhenMatchedListIsEmpty_ReturnsTvShowResponseDtoWithInfo()
    {
        // Arrange
        const string enteredShowName = "Any";
        
        _mockTvShowRepository!
            .Setup(repo => repo
                .GetTvShowByPartialNameAsync(It.IsAny<string>())
            ).ReturnsAsync(new List<TvShow>());

        // Act
        var result = await _tvShowsApiService!.GetTvShowsByPartialNameAsync(enteredShowName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Severity, Is.EqualTo(Severity.Info.ToString()));
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(result.Message, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void GetTvShowsByPartialNameAsync_WithEmptyName_ThrowsParameterException()
    {
        // Arrange
        var tvShowName = string.Empty;
        
        // Act
        async Task ExecutedTask() => await _tvShowsApiService!.GetTvShowsByPartialNameAsync(tvShowName);

        // Assert
        Assert.ThrowsAsync<ParameterException>(ExecutedTask);
    }
}