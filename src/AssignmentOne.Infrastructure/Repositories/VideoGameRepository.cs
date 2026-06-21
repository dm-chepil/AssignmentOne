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
}
