using System;

namespace OneInc.Hackathon.ESBIntegrator.Models;

public class NotifyEndpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; }
    public string Alias { get; set; }
    public string HttpVerb { get; set; }
}
