using AssignmentOne.Api.DTOs;
using AssignmentOne.Core.Interfaces;
using AssignmentOne.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentOne.Api.Controllers;

[ApiController]
[Route("api/video-games")]
public class VideoGamesController : ControllerBase
{
    private const int DefaultPageSize = 5;

    private readonly IVideoGameService _videoGameService;

    public VideoGamesController(IVideoGameService videoGameService)
    {
        _videoGameService = videoGameService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<VideoGameDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        var pagedGames = await _videoGameService.GetPagedAsync(page, pageSize);

        var result = new PagedResult<VideoGameDto>
        {
            Items = pagedGames.Items.Select(vg => new VideoGameDto
            {
                Id = vg.Id,
                Name = vg.Name,
                Description = vg.Description,
                CreateDate = vg.CreateDate
            }),
            TotalCount = pagedGames.TotalCount,
            Page = pagedGames.Page,
            PageSize = pagedGames.PageSize
        };

        return Ok(result);
    }

    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<VideoGameDto>> GetById(Guid gameId)
    {
        var videoGame = await _videoGameService.GetByIdAsync(gameId);

        if (videoGame is null)
            return NotFound();

        var result = new VideoGameDto
        {
            Id = videoGame.Id,
            Name = videoGame.Name,
            Description = videoGame.Description,
            CreateDate = videoGame.CreateDate
        };

        return Ok(result);
    }

    [HttpPut("{gameId:guid}")]
    public async Task<ActionResult<VideoGameDto>> Update(Guid gameId, UpdateVideoGameDto dto)
    {
        var updated = await _videoGameService.UpdateAsync(gameId, dto.Name, dto.Description);

        if (updated is null)
            return NotFound();

        var result = new VideoGameDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Description = updated.Description,
            CreateDate = updated.CreateDate
        };

        return Ok(result);
    }
}
