using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketCuisinesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public TicketCuisinesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/TicketCuisines
    [HttpGet]
    public async Task<IActionResult> GetTicketCuisines()
    {
        var tickets = await _context.TicketCuisines
            .Include(t => t.IdCommandeNavigation)
            .ToListAsync();

        return Ok(tickets);
    }

    // GET: api/TicketCuisines/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketCuisine(int id)
    {
        var ticket = await _context.TicketCuisines
            .Include(t => t.IdCommandeNavigation)
            .FirstOrDefaultAsync(t => t.IdTicket == id);

        if (ticket == null)
            return NotFound("Ticket cuisine introuvable.");

        return Ok(ticket);
    }

    // POST: api/TicketCuisines
    [HttpPost]
    public async Task<IActionResult> CreateTicketCuisine(
        [FromBody] CreateTicketCuisineDto dto)
    {
        // 1. Vérifier que la commande existe
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest("La commande spécifiée n'existe pas.");

        // 2. Vérifier qu'il n'existe pas déjà un ticket
        // pour cette commande
        var ticketExiste = await _context.TicketCuisines
            .AnyAsync(t => t.IdCommande == dto.IdCommande);

        if (ticketExiste)
            return BadRequest(
                "Cette commande possède déjà un ticket cuisine."
            );

        // 3. Vérifier le statut
        var statutsAutorises = new[]
        {
            "En attente",
            "En préparation",
            "Prêt",
            "Servi",
            "Annulée"
        };

        if (!statutsAutorises.Contains(dto.Statut))
        {
            return BadRequest(
                "Statut invalide. Les statuts autorisés sont : " +
                "En attente, En préparation, Prêt, Servi, Annulée."
            );
        }

        // 4. Créer le ticket
        var ticket = new TicketCuisine
        {
            IdCommande = dto.IdCommande,
            DateEnvoi = DateTime.Now,
            Statut = dto.Statut
        };

        _context.TicketCuisines.Add(ticket);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTicketCuisine),
            new { id = ticket.IdTicket },
            ticket
        );
    }

    // PUT: api/TicketCuisines/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicketCuisine(
        int id,
        [FromBody] CreateTicketCuisineDto dto)
    {
        // 1. Vérifier que le ticket existe
        var ticket = await _context.TicketCuisines
            .FirstOrDefaultAsync(t => t.IdTicket == id);

        if (ticket == null)
            return NotFound("Ticket cuisine introuvable.");

        // 2. Vérifier que la commande existe
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest(
                "La commande spécifiée n'existe pas."
            );

        // 3. Vérifier les statuts autorisés
        var statutsAutorises = new[]
        {
            "En attente",
            "En préparation",
            "Prêt",
            "Servi",
            "Annulée"
        };

        if (!statutsAutorises.Contains(dto.Statut))
        {
            return BadRequest(
                "Statut invalide. Les statuts autorisés sont : " +
                "En attente, En préparation, Prêt, Servi, Annulée."
            );
        }

        // 4. Vérifier qu'une autre commande
        // n'utilise pas déjà ce ticket
        var autreTicket = await _context.TicketCuisines
            .AnyAsync(t =>
                t.IdCommande == dto.IdCommande &&
                t.IdTicket != id);

        if (autreTicket)
        {
            return BadRequest(
                "Cette commande possède déjà un autre ticket cuisine."
            );
        }

        // 5. Vérifier les changements de statut
        if (!EstTransitionAutorisee(ticket.Statut, dto.Statut))
        {
            return BadRequest(
                $"Transition de statut impossible : " +
                $"'{ticket.Statut}' → '{dto.Statut}'."
            );
        }

        // 6. Mettre à jour
        ticket.IdCommande = dto.IdCommande;
        ticket.Statut = dto.Statut;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Vérifier si le changement de statut est autorisé
    private bool EstTransitionAutorisee(
        string ancienStatut,
        string nouveauStatut)
    {
        // Aucun changement
        if (ancienStatut == nouveauStatut)
            return true;

        return ancienStatut switch
        {
            "En attente" =>
                nouveauStatut == "En préparation" ||
                nouveauStatut == "Annulée",

            "En préparation" =>
                nouveauStatut == "Prêt" ||
                nouveauStatut == "Annulée",

            "Prêt" =>
                nouveauStatut == "Servi",

            "Servi" =>
                false,

            "Annulée" =>
                false,

            _ => false
        };
    }

    // DELETE: api/TicketCuisines/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicketCuisine(int id)
    {
        var ticket = await _context.TicketCuisines
            .FindAsync(id);

        if (ticket == null)
            return NotFound("Ticket cuisine introuvable.");

        _context.TicketCuisines.Remove(ticket);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}