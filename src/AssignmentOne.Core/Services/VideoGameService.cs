using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Interfaces;
using AssignmentOne.Core.Models;

namespace AssignmentOne.Core.Services;

public class VideoGameService : IVideoGameService
{
    private readonly IVideoGameRepository _repository;

    public VideoGameService(IVideoGameRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<VideoGame>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<PagedResult<VideoGame>> GetPagedAsync(int page, int pageSize)
    {
        return _repository.GetPagedAsync(page, pageSize);
    }

    public Task<VideoGame?> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<VideoGame?> UpdateAsync(Guid id, string name, string description)
    {
        var videoGame = new VideoGame
        {
            Id = id,
            Name = name,
            Description = description
        };

        return _repository.UpdateAsync(videoGame);
    }
}
