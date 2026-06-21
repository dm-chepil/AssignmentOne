using AssignmentOne.Core.Entities;

namespace AssignmentOne.Core.Interfaces;

public interface IVideoGameService
{
    Task<IEnumerable<VideoGame>> GetAllAsync();
}
