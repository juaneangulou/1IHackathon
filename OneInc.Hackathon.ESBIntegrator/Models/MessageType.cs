using System;

namespace OneInc.Hackathon.ESBIntegrator.Models;

public class MessageType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool HasRetries { get; set; }
    public string RetryInterval { get; set; }
    public int MaxRetries { get; set; }
}
