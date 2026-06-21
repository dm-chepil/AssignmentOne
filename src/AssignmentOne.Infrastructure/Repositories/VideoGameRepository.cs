using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Interfaces;
using AssignmentOne.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AssignmentOne.Infrastructure.Repositories;

public class VideoGameRepository : IVideoGameRepository
{
    private readonly AppDbContext _context;

    public VideoGameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VideoGame>> GetAllAsync()
    {
        return await _context.VideoGames.ToListAsync();
    }

    public async Task<VideoGame?> GetByIdAsync(Guid id)
    {
        return await _context.VideoGames.FindAsync(id);
    }

    public async Task<VideoGame?> UpdateAsync(VideoGame videoGame)
    {
        var existing = await _context.VideoGames.FindAsync(videoGame.Id);
        if (existing is null)
            return null;

        existing.Name = videoGame.Name;
        existing.Description = videoGame.Description;

        await _context.SaveChangesAsync();
        return existing;
    }
}
