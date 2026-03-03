using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Data;
using Api.Models;
using Api.ViewModels;
using Api.Utils;
using Api.Extensions;
using Api.ViewModels.Contents;
using Api.ViewModels.Users;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentsController : ControllerBase
{

    private readonly AppDbContext _context;

    public ContentsController(
        AppDbContext context
        )
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
    )
    {
        if (page < 0)
            page = 0;

        if (pageSize <= 0 || pageSize > 25)
            pageSize = 25;

        var total = await _context.Contents.CountAsync();

        var contents = await _context.Contents
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ItemListContentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Tags = x.Tags,
                User = x.User != null ? x.User.Name : "",
                ContentType = x.ContentType != null ? x.ContentType.Slug : ""
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new ResultViewModel<PagedResultViewModel<ItemListContentViewModel>>(
            new PagedResultViewModel<ItemListContentViewModel>(
                contents,
                page,
                pageSize,
                total
            ), null));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id
    )
    {
        var content = await _context.Contents
            .AsNoTracking()
            .Select(x => new DetailContentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Tags = x.Tags,
                CreatedAt = x.CreatedAt,
                UserId = x.UserId,
                ContentTypeId = x.ContentTypeId
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return Ok(new ResultViewModel<DetailContentViewModel>(content, null));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateContentViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var content = new Content
        {
            Name = model.Name,
            Description = model.Description,
            Tags = model.Tags,
            IsOwner = model.IsOwner
        };

        await _context.Contents.AddAsync(content);
        await _context.SaveChangesAsync();

        return Created(
            $"api/[controller]/{content.Id}",
            new ResultViewModel<DetailContentViewModel>(
                new DetailContentViewModel
                {
                    Id = content.Id,
                    Name = content.Name,
                    Description = content.Description,
                    Tags = content.Tags,
                    CreatedAt = content.CreatedAt
                }, null));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] UpdateContentViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var rowsAffected = await _context.Contents
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Name, p => model.Name ?? p.Name)
                .SetProperty(x => x.Description, p => model.Description ?? p.Description)
                .SetProperty(x => x.Tags, p => model.Tags ?? p.Tags)
                .SetProperty(x => x.IsOwner, p => model.IsOwner ?? p.IsOwner)
                .SetProperty(x => x.UpdatedAt, _ => DateTime.UtcNow)
            );

        return rowsAffected == 1 ? NoContent() : NotFound(new ResultViewModel<string>("Content not found"));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id
    )
    {
        var rowAffected = await _context.Contents
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rowAffected == 1 ? NoContent() : BadRequest();
    }
}