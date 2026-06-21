using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Models;

namespace AssignmentOne.Core.Interfaces;

public interface IVideoGameService
{
    Task<IEnumerable<VideoGame>> GetAllAsync();
    Task<PagedResult<VideoGame>> GetPagedAsync(int page, int pageSize);
    Task<VideoGame?> GetByIdAsync(Guid id);
    Task<VideoGame?> UpdateAsync(Guid id, string name, string description);
}
