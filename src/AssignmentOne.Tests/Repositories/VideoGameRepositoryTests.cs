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
}
