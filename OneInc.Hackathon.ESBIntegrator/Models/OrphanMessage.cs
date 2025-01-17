using System;

namespace OneInc.Hackathon.ESBIntegrator.Models;

public class OrphanMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TransactionId { get; set; }
    public string Payload { get; set; }
    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
    public int RemainingAttempts { get; set; } = 3;

    public ESBTransaction Transaction { get; set; }
}
