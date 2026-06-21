using AssignmentOne.Core.Entities;
using AssignmentOne.Core.Interfaces;

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
}
