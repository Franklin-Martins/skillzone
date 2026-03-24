using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Data;
using Api.Models;
using Api.ViewModels;
using Api.ViewModels.ContentTypes;
using Api.Extensions;
using Api.Utils;
using Api.ViewModels.Users;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContentTypesController(AppDbContext context)
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

        var total = await _context.ContentTypes.CountAsync();

        var contentTypes = await _context.ContentTypes
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ItemListContentTypeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                UpdatedAt = x.UpdatedAt,
                CreatedAt = x.CreatedAt
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new ResultViewModel<PagedResultViewModel<ItemListContentTypeViewModel>>(
            new PagedResultViewModel<ItemListContentTypeViewModel>(
                contentTypes,
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
        var contentType = await _context.ContentTypes
            .Select(x => new DetailContentTypeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                UpdatedAt = x.UpdatedAt,
                CreatedAt = x.CreatedAt,
                Contents = x.Contents
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return contentType is not null
        ? Ok(new ResultViewModel<DetailContentTypeViewModel>(contentType, null))
        : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] CreateContentTypeViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var contentType = new ContentType
        {
            Name = model.Name,
            Slug = SlugHelper.Slugify(model.Name)
        };

        await _context.ContentTypes.AddAsync(contentType);
        await _context.SaveChangesAsync();

        return Created(
            $"api/[controller]/{contentType.Id}",
            new ResultViewModel<DetailContentTypeViewModel>(
                new DetailContentTypeViewModel
                {
                    Id = contentType.Id,
                    Name = contentType.Name,
                    Slug = contentType.Slug,
                    UpdatedAt = contentType?.UpdatedAt,
                    CreatedAt = contentType.CreatedAt
                }, null)
            );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] UpdateContentTypeViewModel model

    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var rowAffected = await _context.ContentTypes
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, p => model.Name ?? p.Name)
                .SetProperty(p => p.Slug, p => p.Name != null
                    ? SlugHelper.Slugify(model.Name) : p.Slug)
                .SetProperty(p => p.UpdatedAt, _ => DateTime.UtcNow)
            );

        return rowAffected == 1
        ? NoContent()
        : NotFound(new ResultViewModel<string>("Content type not found!"));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id
    )
    {
        var rowAffected = await _context.ContentTypes
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rowAffected == 1
            ? NoContent()
            : NotFound();
    }
}