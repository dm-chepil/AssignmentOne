using AssignmentOne.Core.Entities;

namespace AssignmentOne.Core.Interfaces;

public interface IVideoGameService
{
    Task<IEnumerable<VideoGame>> GetAllAsync();
    Task<VideoGame?> GetByIdAsync(Guid id);
    Task<VideoGame?> UpdateAsync(Guid id, string name, string description);
}
