using Microsoft.EntityFrameworkCore;
using Api.Models;
using app.Migrations;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ContentType> ContentTypes { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Payment> Payments { get; set; }
}
