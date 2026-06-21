using AssignmentOne.Core.Entities;
using AssignmentOne.Infrastructure.Data;
using AssignmentOne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssignmentOne.Tests.Repositories;

public class VideoGameRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly VideoGameRepository _sut;

    public VideoGameRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _sut = new VideoGameRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_WhenDatabaseIsEmpty_ReturnsEmptyCollection()
    {
        var result = await _sut.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSeededVideoGames()
    {
        var games = new List<VideoGame>
        {
            new() { Id = Guid.NewGuid(), Name = "Game A", Description = "Desc A", CreateDate = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Game B", Description = "Desc B", CreateDate = DateTime.UtcNow }
        };

        _context.VideoGames.AddRange(games);
        await _context.SaveChangesAsync();

        var result = await _sut.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCorrectData()
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;

        _context.VideoGames.Add(new VideoGame
        {
            Id = id,
            Name = "Test Game",
            Description = "Test Description",
            CreateDate = createDate
        });
        await _context.SaveChangesAsync();

        var result = (await _sut.GetAllAsync()).Single();

        Assert.Equal(id, result.Id);
        Assert.Equal("Test Game", result.Name);
        Assert.Equal("Test Description", result.Description);
        Assert.Equal(createDate, result.CreateDate);
    }

    [Fact]
    public async Task GetPagedAsync_WhenDatabaseIsEmpty_ReturnsEmptyPagedResult()
    {
        var result = await _sut.GetPagedAsync(1, 5);

        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsCorrectPageOfItems()
    {
        var games = Enumerable.Range(1, 10).Select(i => new VideoGame
        {
            Id = Guid.NewGuid(),
            Name = $"Game {i}",
            Description = $"Desc {i}",
            CreateDate = new DateTime(2024, 1, i, 0, 0, 0, DateTimeKind.Utc)
        }).ToList();

        _context.VideoGames.AddRange(games);
        await _context.SaveChangesAsync();

        var result = await _sut.GetPagedAsync(page: 2, pageSize: 3);

        Assert.Equal(3, result.Items.Count());
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(2, result.Page);
        Assert.Equal(3, result.PageSize);
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsTotalCountForAllRecords()
    {
        var games = Enumerable.Range(1, 7).Select(i => new VideoGame
        {
            Id = Guid.NewGuid(),
            Name = $"Game {i}",
            Description = $"Desc {i}",
            CreateDate = DateTime.UtcNow
        }).ToList();

        _context.VideoGames.AddRange(games);
        await _context.SaveChangesAsync();

        var result = await _sut.GetPagedAsync(page: 1, pageSize: 5);

        Assert.Equal(7, result.TotalCount);
        Assert.Equal(5, result.Items.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameExists_ReturnsVideoGame()
    {
        var id = Guid.NewGuid();
        _context.VideoGames.Add(new VideoGame { Id = id, Name = "Game A", Description = "Desc A", CreateDate = DateTime.UtcNow });
        await _context.SaveChangesAsync();

        var result = await _sut.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameExists_ReturnsCorrectFields()
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;
        _context.VideoGames.Add(new VideoGame { Id = id, Name = "Game A", Description = "Desc A", CreateDate = createDate });
        await _context.SaveChangesAsync();

        var result = await _sut.GetByIdAsync(id);

        Assert.Equal("Game A", result!.Name);
        Assert.Equal("Desc A", result.Description);
        Assert.Equal(createDate, result.CreateDate);
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameDoesNotExist_ReturnsNull()
    {
        var result = await _sut.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameExists_ReturnsUpdatedVideoGame()
    {
        var id = Guid.NewGuid();
        _context.VideoGames.Add(new VideoGame { Id = id, Name = "Original", Description = "Original Desc", CreateDate = DateTime.UtcNow });
        await _context.SaveChangesAsync();

        var updated = new VideoGame { Id = id, Name = "Updated", Description = "Updated Desc" };
        var result = await _sut.UpdateAsync(updated);

        Assert.NotNull(result);
        Assert.Equal("Updated", result.Name);
        Assert.Equal("Updated Desc", result.Description);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameExists_PersistsChangesToDatabase()
    {
        var id = Guid.NewGuid();
        _context.VideoGames.Add(new VideoGame { Id = id, Name = "Original", Description = "Original Desc", CreateDate = DateTime.UtcNow });
        await _context.SaveChangesAsync();

        await _sut.UpdateAsync(new VideoGame { Id = id, Name = "Updated", Description = "Updated Desc" });

        var persisted = await _context.VideoGames.FindAsync(id);
        Assert.Equal("Updated", persisted!.Name);
        Assert.Equal("Updated Desc", persisted.Description);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameExists_DoesNotOverwriteCreateDate()
    {
        var id = Guid.NewGuid();
        var originalDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        _context.VideoGames.Add(new VideoGame { Id = id, Name = "Original", Description = "Desc", CreateDate = originalDate });
        await _context.SaveChangesAsync();

        var result = await _sut.UpdateAsync(new VideoGame { Id = id, Name = "Updated", Description = "Updated Desc" });

        Assert.Equal(originalDate, result!.CreateDate);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameDoesNotExist_ReturnsNull()
    {
        var result = await _sut.UpdateAsync(new VideoGame { Id = Guid.NewGuid(), Name = "X", Description = "Y" });

        Assert.Null(result);
    }
}
