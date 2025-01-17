using Microsoft.AspNetCore.Mvc;
using OneInc.Hackathon.ESBIntegrator.Data;
using OneInc.Hackathon.ESBIntegrator.Models;

namespace OneInc.Hackathon.ESBIntegrator.Controllers;

[ApiController]
[Route("api/transactions")]
public class ESBTransactionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ESBTransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] string type)
    {
        if (string.IsNullOrEmpty(type))
        {
            return BadRequest("Transaction type is required.");
        }

        var transaction = new ESBTransaction
        {
            Type = type
        };

        _context.ESBTransactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(new { TransactionId = transaction.TransactionId });
    }

    [HttpGet("{transactionId:guid}")]
    public async Task<IActionResult> GetTransaction(Guid transactionId)
    {
        var transaction = await _context.ESBTransactions.FindAsync(transactionId);
        if (transaction == null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }
}
