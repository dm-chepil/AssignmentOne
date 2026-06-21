using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Models;

namespace AssignmentOne.Core.Interfaces;

public interface IVideoGameRepository
{
    Task<IEnumerable<VideoGame>> GetAllAsync();
    Task<PagedResult<VideoGame>> GetPagedAsync(int page, int pageSize);
    Task<VideoGame?> GetByIdAsync(Guid id);
    Task<VideoGame?> UpdateAsync(VideoGame videoGame);
}
