using AssignmentOne.Api.DTOs;
using AssignmentOne.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentOne.Api.Controllers;

[ApiController]
[Route("api/video-games")]
public class VideoGamesController : ControllerBase
{
    private readonly IVideoGameService _videoGameService;

    public VideoGamesController(IVideoGameService videoGameService)
    {
        _videoGameService = videoGameService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameDto>>> GetAll()
    {
        var videoGames = await _videoGameService.GetAllAsync();

        var result = videoGames.Select(vg => new VideoGameDto
        {
            Id = vg.Id,
            Name = vg.Name,
            Description = vg.Description,
            CreateDate = vg.CreateDate
        });

        return Ok(result);
    }
}
