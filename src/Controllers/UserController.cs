using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Users.ToList());
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }
}
