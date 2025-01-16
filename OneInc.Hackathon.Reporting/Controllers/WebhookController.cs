using Microsoft.AspNetCore.Mvc;
using OneInc.Hackathon.SharedDatabase.Data;
using OneInc.Hackathon.SharedDatabase.Models;

namespace OneInc.Hackathon.Reporting.Controllers;

[ApiController]
[Route("api/webhook")]
public class WebhookController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WebhookController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Receive([FromBody] object payload, [FromHeader(Name = "TransactionId")] Guid? transactionId)
    {
        
        if (payload == null)
        {
            return BadRequest("Payload cannot be null.");
        }

        
        if (transactionId == null || transactionId == Guid.Empty)
        {
            return BadRequest("TransactionId header is missing or invalid.");
        }

        
        var webhookPayload = new WebhookPayload
        {
            Id = Guid.NewGuid(),
            JsonContent = payload.ToString(),
            ReceivedAt = DateTime.UtcNow,
            TransactionId = transactionId.Value
        };

        _context.WebhookPayloads.Add(webhookPayload);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Payload received", Id = webhookPayload.Id, TransactionId = webhookPayload.TransactionId });
    }
}
