using Api.Data;
using Api.Extensions;
using Api.Models;
using Api.Utils;
using Api.ViewModels;
using Api.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var roles = await _context.Roles.AsNoTracking().ToListAsync();

        return Ok(roles);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id
    )
    {
        var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (role is null)
            return NotFound();

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] CreateRoleViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        if (model is null)
            return BadRequest(new ResultViewModel<string>("[Name] is required"));

        var role = new Role
        {
            Name = model.Name,
            Slug = SlugHelper.Slugify(model.Name)
        };
           
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();

        return Created(
            $"api/roles/{role.Id}",
            new ResultViewModel<Role>(role, null)
            );
    }
}
