using Microsoft.AspNetCore.Mvc;
using OneInc.Hackathon.ESBIntegrator.Data;
using OneInc.Hackathon.ESBIntegrator.Models;

namespace OneInc.Hackathon.ESBIntegrator.Controllers;

[ApiController]
[Route("api/endpoints")]
public class NotifyEndpointController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotifyEndpointController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllEndpoints()
    {
        var endpoints = _context.Endpoints.ToList();
        return Ok(endpoints);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEndpoint([FromBody] NotifyEndpoint endpoint)
    {
        if (string.IsNullOrEmpty(endpoint.Url) || string.IsNullOrEmpty(endpoint.HttpVerb))
        {
            return BadRequest("Url and HttpVerb are required.");
        }

        _context.Endpoints.Add(endpoint);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAllEndpoints), new { endpoint.Id }, endpoint);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEndpoint(Guid id)
    {
        var endpoint = await _context.Endpoints.FindAsync(id);
        if (endpoint == null)
        {
            return NotFound();
        }

        _context.Endpoints.Remove(endpoint);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
