using Api.Extensions;
using Api.Models;
using Api.Models.Enums;
using Api.ViewModels;
using Api.ViewModels.Plans;
using app.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class PlansController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [Authorize]
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

        var total = await _context.Plans.CountAsync();

        var plans = await _context.Plans
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new ResultViewModel<List<Plan>>(plans, null));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id
    )
    {
        var plan = await _context.Plans
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new DetailPlanViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
                Username = x.User.Name,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                DurationInMonths = x.DurationInMonths,
                UpdatedAt = x.UpdatedAt,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (plan is null)
            return NotFound();
        
        return Ok(new ResultViewModel<DetailPlanViewModel>(plan, null));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreatePlanViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new ResultViewModel<string>("User not authenticated"));

        var userId = Guid.Parse(userIdClaim);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found"));

        var plan = new Plan
        {
            UserId = userId,
            IsOwner = true,
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            Status = EPlanStatus.Pending,
            DurationInMonths = model.DurationInMonths
        };

        await _context.Plans.AddAsync(plan);
        await _context.SaveChangesAsync();

        return Created($"api/plans/{plan.Id}", new ResultViewModel<Plan>(plan, null));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var rows = await _context.Plans
            .Where(x => x.Id == id && x.UserId == Guid.Parse(userId))
            .ExecuteDeleteAsync();

        return rows == 1
            ? NoContent()
            : NotFound();
    }
}
