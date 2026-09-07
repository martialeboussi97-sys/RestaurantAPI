using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public ClientsController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Clients
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _context.Clients.ToListAsync();

        return Ok(clients);
    }

    // GET: api/Clients/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
            return NotFound("Client introuvable.");

        return Ok(client);
    }

    // POST: api/Clients
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
    {
        var client = new Client
        {
            Nom = dto.Nom,
            Prenom = dto.Prenom,
            Telephone = dto.Telephone,
            Email = dto.Email
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetClient),
            new { id = client.IdClient },
            client
        );
    }

    // PUT: api/Clients/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(
        int id,
        [FromBody] CreateClientDto dto)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
            return NotFound("Client introuvable.");

        client.Nom = dto.Nom;
        client.Prenom = dto.Prenom;
        client.Telephone = dto.Telephone;
        client.Email = dto.Email;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Clients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
            return NotFound("Client introuvable.");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}