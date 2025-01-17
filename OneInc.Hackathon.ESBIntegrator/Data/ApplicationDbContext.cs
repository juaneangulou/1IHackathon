using Microsoft.EntityFrameworkCore;
using OneInc.Hackathon.ESBIntegrator.Models;

namespace OneInc.Hackathon.ESBIntegrator.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ESBTransaction> ESBTransactions { get; set; }
    public DbSet<NotifyEndpoint> Endpoints { get; set; }
    public DbSet<Rule> Rules { get; set; }
    public DbSet<MessageType> MessageTypes { get; set; }
    public DbSet<OrphanMessage> OrphanMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ESBTransaction>()
            .HasMany(t => t.OrphanMessages)
            .WithOne(o => o.Transaction)
            .HasForeignKey(o => o.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);

        var endpoint1 = new NotifyEndpoint { Id = Guid.NewGuid(), Url = "http://localhost:5101/api/webhook", Alias = "Notifications", HttpVerb = "POST" };
        var endpoint2 = new NotifyEndpoint { Id = Guid.NewGuid(), Url = "http://localhost:5102/api/webhook", Alias = "Webhooks", HttpVerb = "POST" };
        var endpoint3 = new NotifyEndpoint { Id = Guid.NewGuid(), Url = "http://localhost:5103/api/webhook", Alias = "Reporting", HttpVerb = "POST" };

        modelBuilder.Entity<NotifyEndpoint>().HasData(endpoint1, endpoint2, endpoint3);

        var messageTypeNotification = new MessageType { Id = Guid.NewGuid(), Name = "Notification", HasRetries = true, RetryInterval = "1M,2M,3M", MaxRetries = 3 };
        var messageTypeError = new MessageType { Id = Guid.NewGuid(), Name = "Error", HasRetries = true, RetryInterval = "5M,10M", MaxRetries = 2 };
        var messageTypeBroadcast = new MessageType { Id = Guid.NewGuid(), Name = "Broadcast", HasRetries = false, RetryInterval = "1M", MaxRetries = 0 };

        modelBuilder.Entity<MessageType>().HasData(messageTypeNotification, messageTypeError, messageTypeBroadcast);

        
        modelBuilder.Entity<Rule>().HasData(
    
            new Rule { Id = Guid.NewGuid(), MessageType = "Notification", EndpointId = endpoint1.Id },

    
            new Rule { Id = Guid.NewGuid(), MessageType = "Error", EndpointId = endpoint1.Id },
            new Rule { Id = Guid.NewGuid(), MessageType = "Error", EndpointId = endpoint2.Id },

    
            new Rule { Id = Guid.NewGuid(), MessageType = "Broadcast", EndpointId = endpoint1.Id },
            new Rule { Id = Guid.NewGuid(), MessageType = "Broadcast", EndpointId = endpoint2.Id },
            new Rule { Id = Guid.NewGuid(), MessageType = "Broadcast", EndpointId = endpoint3.Id }
        );
    }
}
