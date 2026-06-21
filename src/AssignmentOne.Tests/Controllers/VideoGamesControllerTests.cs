using AssignmentOne.Api.Controllers;
using AssignmentOne.Api.DTOs;
using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssignmentOne.Tests.Controllers;

public class VideoGamesControllerTests
{
    private readonly Mock<IVideoGameService> _serviceMock;
    private readonly VideoGamesController _sut;

    public VideoGamesControllerTests()
    {
        _serviceMock = new Mock<IVideoGameService>();
        _sut = new VideoGamesController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        _serviceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(Enumerable.Empty<VideoGame>());

        var result = await _sut.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_ReturnsAllVideoGamesAsDtos()
    {
        var games = new List<VideoGame>
        {
            new() { Id = Guid.NewGuid(), Name = "Game A", Description = "Desc A", CreateDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Game B", Description = "Desc B", CreateDate = DateTime.UtcNow }
        };

        _serviceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(games);

        var result = await _sut.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dtos = Assert.IsAssignableFrom<IEnumerable<VideoGameDto>>(ok.Value);
        Assert.Equal(2, dtos.Count());
    }

    [Fact]
    public async Task GetAll_MapsDtoFieldsCorrectly()
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;

        _serviceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<VideoGame>
            {
                new() { Id = id, Name = "Test Game", Description = "Test Desc", CreateDate = createDate }
            });

        var result = await _sut.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsAssignableFrom<IEnumerable<VideoGameDto>>(ok.Value).Single();

        Assert.Equal(id, dto.Id);
        Assert.Equal("Test Game", dto.Name);
        Assert.Equal("Test Desc", dto.Description);
        Assert.Equal(createDate, dto.CreateDate);
    }

    [Fact]
    public async Task GetAll_WhenNoGamesExist_ReturnsEmptyCollection()
    {
        _serviceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(Enumerable.Empty<VideoGame>());

        var result = await _sut.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dtos = Assert.IsAssignableFrom<IEnumerable<VideoGameDto>>(ok.Value);
        Assert.Empty(dtos);
    }

    [Fact]
    public async Task GetAll_CallsServiceOnce()
    {
        _serviceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(Enumerable.Empty<VideoGame>());

        await _sut.GetAll();

        _serviceMock.Verify(s => s.GetAllAsync(), Times.Once);
    }
}
