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

    [Fact]
    public async Task GetById_WhenGameExists_ReturnsOkResult()
    {
        var id = Guid.NewGuid();
        _serviceMock
            .Setup(s => s.GetByIdAsync(id))
            .ReturnsAsync(new VideoGame { Id = id, Name = "Game A", Description = "Desc A", CreateDate = DateTime.UtcNow });

        var result = await _sut.GetById(id);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetById_WhenGameExists_MapsDtoFieldsCorrectly()
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;
        _serviceMock
            .Setup(s => s.GetByIdAsync(id))
            .ReturnsAsync(new VideoGame { Id = id, Name = "Game A", Description = "Desc A", CreateDate = createDate });

        var result = await _sut.GetById(id);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<VideoGameDto>(ok.Value);
        Assert.Equal(id, dto.Id);
        Assert.Equal("Game A", dto.Name);
        Assert.Equal("Desc A", dto.Description);
        Assert.Equal(createDate, dto.CreateDate);
    }

    [Fact]
    public async Task GetById_WhenGameDoesNotExist_ReturnsNotFound()
    {
        _serviceMock
            .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((VideoGame?)null);

        var result = await _sut.GetById(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_CallsServiceWithCorrectId()
    {
        var id = Guid.NewGuid();
        _serviceMock
            .Setup(s => s.GetByIdAsync(id))
            .ReturnsAsync(new VideoGame { Id = id });

        await _sut.GetById(id);

        _serviceMock.Verify(s => s.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task Update_WhenGameExists_ReturnsOkResult()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateVideoGameDto { Name = "Updated", Description = "Updated Desc" };
        _serviceMock
            .Setup(s => s.UpdateAsync(id, dto.Name, dto.Description))
            .ReturnsAsync(new VideoGame { Id = id, Name = dto.Name, Description = dto.Description, CreateDate = DateTime.UtcNow });

        var result = await _sut.Update(id, dto);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task Update_WhenGameExists_ReturnsUpdatedDto()
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;
        var dto = new UpdateVideoGameDto { Name = "Updated", Description = "Updated Desc" };
        _serviceMock
            .Setup(s => s.UpdateAsync(id, dto.Name, dto.Description))
            .ReturnsAsync(new VideoGame { Id = id, Name = dto.Name, Description = dto.Description, CreateDate = createDate });

        var result = await _sut.Update(id, dto);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<VideoGameDto>(ok.Value);
        Assert.Equal(id, returned.Id);
        Assert.Equal("Updated", returned.Name);
        Assert.Equal("Updated Desc", returned.Description);
        Assert.Equal(createDate, returned.CreateDate);
    }

    [Fact]
    public async Task Update_WhenGameDoesNotExist_ReturnsNotFound()
    {
        var dto = new UpdateVideoGameDto { Name = "Updated", Description = "Updated Desc" };
        _serviceMock
            .Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((VideoGame?)null);

        var result = await _sut.Update(Guid.NewGuid(), dto);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Update_CallsServiceWithCorrectArguments()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateVideoGameDto { Name = "Updated", Description = "Updated Desc" };
        _serviceMock
            .Setup(s => s.UpdateAsync(id, dto.Name, dto.Description))
            .ReturnsAsync(new VideoGame { Id = id, Name = dto.Name, Description = dto.Description });

        await _sut.Update(id, dto);

        _serviceMock.Verify(s => s.UpdateAsync(id, dto.Name, dto.Description), Times.Once);
    }
}
