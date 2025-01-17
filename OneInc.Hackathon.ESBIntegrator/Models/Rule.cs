using System;

namespace OneInc.Hackathon.ESBIntegrator.Models;

public class Rule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string MessageType { get; set; }
    public Guid EndpointId { get; set; }
}
