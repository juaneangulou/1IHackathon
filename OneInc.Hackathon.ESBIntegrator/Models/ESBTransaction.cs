using System;
using System.Collections.Generic;

namespace OneInc.Hackathon.ESBIntegrator.Models;

public class ESBTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TransactionId { get; set; } = Guid.NewGuid();
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<OrphanMessage> OrphanMessages { get; set; }
}
