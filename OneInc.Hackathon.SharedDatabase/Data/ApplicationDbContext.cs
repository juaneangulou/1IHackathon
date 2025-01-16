using Microsoft.EntityFrameworkCore;
using OneInc.Hackathon.SharedDatabase.Models;

namespace OneInc.Hackathon.SharedDatabase.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<WebhookPayload> WebhookPayloads { get; set; }
}
