using AssignmentOne.Core.Entities;

namespace AssignmentOne.Core.Interfaces;

public interface IVideoGameRepository
{
    Task<IEnumerable<VideoGame>> GetAllAsync();
    Task<VideoGame?> GetByIdAsync(Guid id);
    Task<VideoGame?> UpdateAsync(VideoGame videoGame);
}
