using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CommandesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Commandes
    [HttpGet]
    public async Task<IActionResult> GetCommandes()
    {
        var commandes = await _context.Commandes
            .Include(c => c.IdClientNavigation)
            .ToListAsync();

        return Ok(commandes);
    }

    // GET: api/Commandes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommande(int id)
    {
        var commande = await _context.Commandes
            .Include(c => c.IdClientNavigation)
            .FirstOrDefaultAsync(c => c.IdCommande == id);

        if (commande == null)
            return NotFound("Commande introuvable.");

        return Ok(commande);
    }

    // POST: api/Commandes
    [HttpPost]
    public async Task<IActionResult> CreateCommande(
        [FromBody] CreateCommandeDto dto)
    {
        // Vérifier que le client existe uniquement s'il est renseigné
        if (dto.IdClient.HasValue)
        {
            var clientExiste = await _context.Clients
                .AnyAsync(c => c.IdClient == dto.IdClient.Value);

            if (!clientExiste)
                return BadRequest("Le client spécifié n'existe pas.");
        }

        var commande = new Commande
        {
            DateCommande = DateTime.Now,
            IdClient = dto.IdClient,
            IdTable = dto.IdTable,
            IdServeur = dto.IdServeur,
            Statut = dto.Statut,
            Remarque = dto.Remarque,
            IdTypeCommande = dto.IdTypeCommande
        };

        _context.Commandes.Add(commande);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCommande),
            new { id = commande.IdCommande },
            commande
        );
    }

    // PUT: api/Commandes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCommande(
        int id,
        [FromBody] CreateCommandeDto dto)
    {
        var commande = await _context.Commandes.FindAsync(id);

        if (commande == null)
            return NotFound("Commande introuvable.");

        // Vérifier que le client existe uniquement s'il est renseigné
        if (dto.IdClient.HasValue)
        {
            var clientExiste = await _context.Clients
                .AnyAsync(c => c.IdClient == dto.IdClient.Value);

            if (!clientExiste)
                return BadRequest("Le client spécifié n'existe pas.");
        }

        commande.IdClient = dto.IdClient;
        commande.IdTable = dto.IdTable;
        commande.IdServeur = dto.IdServeur;
        commande.Statut = dto.Statut;
        commande.Remarque = dto.Remarque;
        commande.IdTypeCommande = dto.IdTypeCommande;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    // PUT: api/Commandes/5/statut
    [HttpPut("{id}/statut")]
    public async Task<IActionResult> UpdateStatutCommande(
        int id,
        [FromBody] UpdateStatutCommandeDto dto)
    {
        var commande = await _context.Commandes.FindAsync(id);

        if (commande == null)
            return NotFound("Commande introuvable.");

        var statutsAutorises = new[]
        {
        "En attente",
        "En préparation",
        "Prête",
        "Servie",
        "Annulée"
    };

        if (!statutsAutorises.Contains(dto.Statut))
            return BadRequest("Statut de commande invalide.");

        commande.Statut = dto.Statut;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Statut de la commande mis à jour.",
            idCommande = commande.IdCommande,
            statut = commande.Statut
        });
    }

    // DELETE: api/Commandes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCommande(int id)
    {
        var commande = await _context.Commandes.FindAsync(id);

        if (commande == null)
            return NotFound("Commande introuvable.");

        _context.Commandes.Remove(commande);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}