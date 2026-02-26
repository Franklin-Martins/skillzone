using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Api.Data;
using Api.Models;
using Api.ViewModels;
using Api.Utils;
using Api.Extensions;
using Api.ViewModels.Users;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public UsersController(
        AppDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int page = page < 0 ? 0 : page,
        [FromQuery] int pageSize = pageSize > 25 ? 25 : pageSize
    )
    {
        if (page < 0)
            page = 0;
            
        if (pageSize <= 0 || pageSize > 25)
            pageSize = 25;

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

        return Ok(new ResultViewModel<List<ListUsersViewModel>>(users, null));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id
    )
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
        [FromBody] CreateUserViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        if (await GetUserByEmail(model.Email) is not null)
            return BadRequest(new ResultViewModel<string>("Email already exists"));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = _passwordHasher.HashPassword(user, model.Password),
            Slug = $"{SlugHelper.Slugify(model.Name)}-{Guid.NewGuid()}"
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Created($"api/users/{user.Id}",
            new ResultViewModel<string>("Usuário criado com sucesso!", null));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateUserViewModel model,
        [FromRoute] Guid id
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return NotFound();

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
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id
    )
    {
        var rowAffected = await _context.Users
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return rowAffected == 1 ? NoContent() : NotFound();
    }

    private async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}
