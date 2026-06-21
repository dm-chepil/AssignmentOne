using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Interfaces;
using AssignmentOne.Core.Services;
using Moq;

namespace AssignmentOne.Tests.Services;

public class VideoGameServiceTests
{
    private readonly Mock<IVideoGameRepository> _repositoryMock;
    private readonly VideoGameService _sut;

    public VideoGameServiceTests()
    {
        _repositoryMock = new Mock<IVideoGameRepository>();
        _sut = new VideoGameService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllVideoGames()
    {
        var expected = new List<VideoGame>
        {
            new() { Id = Guid.NewGuid(), Name = "Game A", Description = "Desc A", CreateDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Game B", Description = "Desc B", CreateDate = DateTime.UtcNow }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(expected);

        var result = await _sut.GetAllAsync();

        Assert.Equal(expected, result);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_WhenNoGamesExist_ReturnsEmptyCollection()
    {
        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(Enumerable.Empty<VideoGame>());

        var result = await _sut.GetAllAsync();

        Assert.Empty(result);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_DelegatesToRepository()
    {
        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<VideoGame>());

        await _sut.GetAllAsync();

        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
