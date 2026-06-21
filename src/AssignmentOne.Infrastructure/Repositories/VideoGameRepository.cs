using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Interfaces;
using AssignmentOne.Core.Models;
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

    public async Task<PagedResult<VideoGame>> GetPagedAsync(int page, int pageSize)
    {
        var totalCount = await _context.VideoGames.CountAsync();

        var items = await _context.VideoGames
            .OrderBy(vg => vg.CreateDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<VideoGame>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
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
