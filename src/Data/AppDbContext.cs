using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserContentProgress> UserContentProgresses { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<MembershipContent> MembershipContents { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}
