using Api.Data;
using Api.Extensions;
using Api.Models;
using Api.ViewModels;
using Api.ViewModels.Payments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
    )
    {
        var total = await _context.Payments.CountAsync();

        var payments = await _context.Payments
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ListPaymentViewModel
            {
                Id = x.Id,
                PlanId = x.PlanId,
                PlanName = x.Plan.Name,
                Type = x.Type,
                Status = x.Status,
                Amount = x.Amount,
                CreatedAt = x.CreatedAt
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new ResultViewModel<PagedResultViewModel<ListPaymentViewModel>>(
            new PagedResultViewModel<ListPaymentViewModel>(
                payments,
                page,
                pageSize,
                total
            ), null));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var payment = await _context.Payments
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new DetailPaymentViewModel
            {
                Id = x.Id,
                PlanId = x.PlanId,
                Type = x.Type,
                Status = x.Status,
                Amount = x.Amount,
                CreatedAt = x.CreatedAt,
                PaidAt = x.PaidAt
            })
            .FirstOrDefaultAsync();

        if (payment == null)
            return NotFound();

        return Ok(new ResultViewModel<DetailPaymentViewModel>(payment, null));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreatePaymentViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var plan = await _context.Plans.FirstOrDefaultAsync(x => x.Id == model.PlanId);

        if (plan == null)
            return NotFound("Plan not found");

        var payment = new Payment
        {
            PlanId = model.PlanId,
            Type = model.Type,
            Amount = model.Amount,
            Status = EPaymentStatus.Pending
        };

        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();

        return Created(
            $"api/payments/{payment.Id}",
            payment
        );
    }
}