using Api.Data;
using Api.Extensions;
using Api.Models;
using Api.Services;
using Api.ViewModels;
using Api.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    AppDbContext context,
    TokenService tokenService
) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly TokenService _tokenService = tokenService;
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    [HttpPost("token")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginAuthViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var user = await _context.Users
            .Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            return Unauthorized(new ResultViewModel<string>("Invalid credentials"));

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            model.Password
        );

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized(new ResultViewModel<string>("Invalid credentials"));

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            await _context.SaveChangesAsync();
        }

        var token = _tokenService.GenerateToken(user);

        return Ok(new ResultViewModel<dynamic>(
            new {
                Access = token,
                Refresh = "TODO"
            }, null));
    }
}
