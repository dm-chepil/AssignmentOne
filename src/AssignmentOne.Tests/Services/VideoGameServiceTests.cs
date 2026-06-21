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

    [Fact]
    public async Task GetByIdAsync_WhenGameExists_ReturnsVideoGame()
    {
        var id = Guid.NewGuid();
        var expected = new VideoGame { Id = id, Name = "Game A", Description = "Desc A", CreateDate = DateTime.UtcNow };
        _repositoryMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(expected);

        var result = await _sut.GetByIdAsync(id);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameDoesNotExist_ReturnsNull()
    {
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((VideoGame?)null);

        var result = await _sut.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_DelegatesToRepositoryWithCorrectId()
    {
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(new VideoGame { Id = id });

        await _sut.GetByIdAsync(id);

        _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameExists_ReturnsUpdatedVideoGame()
    {
        var id = Guid.NewGuid();
        var expected = new VideoGame { Id = id, Name = "Updated", Description = "Updated Desc", CreateDate = DateTime.UtcNow };
        _repositoryMock
            .Setup(r => r.UpdateAsync(It.Is<VideoGame>(g => g.Id == id && g.Name == "Updated" && g.Description == "Updated Desc")))
            .ReturnsAsync(expected);

        var result = await _sut.UpdateAsync(id, "Updated", "Updated Desc");

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameDoesNotExist_ReturnsNull()
    {
        _repositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<VideoGame>()))
            .ReturnsAsync((VideoGame?)null);

        var result = await _sut.UpdateAsync(Guid.NewGuid(), "Updated", "Updated Desc");

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_PassesCorrectEntityToRepository()
    {
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<VideoGame>()))
            .ReturnsAsync(new VideoGame { Id = id });

        await _sut.UpdateAsync(id, "My Game", "My Desc");

        _repositoryMock.Verify(r => r.UpdateAsync(
            It.Is<VideoGame>(g => g.Id == id && g.Name == "My Game" && g.Description == "My Desc")),
            Times.Once);
    }
}
