using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Api.Data;
using Api.Models;
using Api.ViewModels;
using Api.Utils;
using Api.Extensions;
using Api.ViewModels.Users;

namespace app.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

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

        var total = await _context.Users.CountAsync();

        var users = await _context.Users
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ListUsersViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new ResultViewModel<PagedResultViewModel<ListUsersViewModel>>(
            new PagedResultViewModel<ListUsersViewModel>(
                users,
                page,
                pageSize,
                total
            ), null));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new DetailUserViewModel
            {
                Name = x.Name,
                Email = x.Email,
                Slug = x.Slug
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found"));

        return Ok(new ResultViewModel<DetailUserViewModel>(user, null));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        if (await GetUserByEmail(model.Email) is not null)
            return BadRequest(new ResultViewModel<string>("Email already exists"));

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Email = model.Email,
            Slug = $"{SlugHelper.Slugify(model.Name)}-{Guid.NewGuid()}",
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Created($"api/users/{user.Id}",
            new ResultViewModel<string>("Usuário criado com sucesso!"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found"));

        if (model.Name != null)
        {
            user.Name = model.Name;
            user.Slug = $"{SlugHelper.Slugify(model.Name)}-{Guid.NewGuid()}";
        }

        if (model.Email != null)
            user.Email = model.Email;

        if (model.Password != null)
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var rows = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rows == 1
            ? NoContent()
            : NotFound(new ResultViewModel<string>("User not found"));
    }

    private async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}