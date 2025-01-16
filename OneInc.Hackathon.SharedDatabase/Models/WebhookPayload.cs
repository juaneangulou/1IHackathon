namespace OneInc.Hackathon.SharedDatabase.Models;

public class WebhookPayload
{
    public Guid Id { get; set; }
    public string JsonContent { get; set; }
    public DateTime ReceivedAt { get; set; }
    public Guid TransactionId { get; set; }
}
