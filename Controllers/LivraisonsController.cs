using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LivraisonsController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public LivraisonsController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Livraisons
    [HttpGet]
    public async Task<IActionResult> GetLivraisons()
    {
        var livraisons = await _context.Livraisons
            .Include(l => l.IdCommandeNavigation)
            .Include(l => l.IdLivreurNavigation)
            .ToListAsync();

        return Ok(livraisons);
    }

    // GET: api/Livraisons/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLivraison(int id)
    {
        var livraison = await _context.Livraisons
            .Include(l => l.IdCommandeNavigation)
            .Include(l => l.IdLivreurNavigation)
            .FirstOrDefaultAsync(l => l.IdLivraison == id);

        if (livraison == null)
            return NotFound("Livraison introuvable.");

        return Ok(livraison);
    }

    // POST: api/Livraisons
    [HttpPost]
    public async Task<IActionResult> CreateLivraison(
        [FromBody] CreateLivraisonDto dto)
    {
        // 1. Vérifier la commande
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest("La commande spécifiée n'existe pas.");

        // 2. Vérifier qu'il n'existe pas déjà une livraison
        // pour cette commande
        var livraisonExiste = await _context.Livraisons
            .AnyAsync(l => l.IdCommande == dto.IdCommande);

        if (livraisonExiste)
            return BadRequest(
                "Cette commande possède déjà une livraison."
            );

        // 3. Vérifier l'adresse
        if (string.IsNullOrWhiteSpace(dto.AdresseLivraison))
            return BadRequest(
                "L'adresse de livraison est obligatoire."
            );

        // 4. Vérifier le téléphone
        if (string.IsNullOrWhiteSpace(dto.TelephoneClient))
            return BadRequest(
                "Le numéro de téléphone du client est obligatoire."
            );

        // 5. Vérifier les frais de livraison
        if (dto.FraisLivraison.HasValue && dto.FraisLivraison.Value < 0)
            return BadRequest(
                "Les frais de livraison ne peuvent pas être négatifs."
            );

        // 6. Vérifier le statut
        var statutsAutorises = new[]
        {
            "En attente",
            "En préparation",
            "En livraison",
            "Livrée",
            "Annulée"
        };

        if (!statutsAutorises.Contains(dto.Statut))
            return BadRequest(
                "Statut invalide. Les statuts autorisés sont : " +
                "En attente, En préparation, En livraison, Livrée, Annulée."
            );

        // 7. Vérifier le livreur s'il est fourni
        if (dto.IdLivreur.HasValue)
        {
            var livreurExiste = await _context.Livreurs
                .AnyAsync(l => l.IdLivreur == dto.IdLivreur.Value);

            if (!livreurExiste)
                return BadRequest(
                    "Le livreur spécifié n'existe pas."
                );
        }

        // 8. Créer la livraison
        var livraison = new Livraison
        {
            IdCommande = dto.IdCommande,
            AdresseLivraison = dto.AdresseLivraison.Trim(),
            TelephoneClient = dto.TelephoneClient.Trim(),
            DateLivraison = DateTime.Now,
            Statut = dto.Statut,
            FraisLivraison = dto.FraisLivraison,
            Remarque = dto.Remarque,
            IdLivreur = dto.IdLivreur
        };

        _context.Livraisons.Add(livraison);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetLivraison),
            new { id = livraison.IdLivraison },
            livraison
        );
    }

    // PUT: api/Livraisons/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLivraison(
        int id,
        [FromBody] CreateLivraisonDto dto)
    {
        // 1. Vérifier la livraison
        var livraison = await _context.Livraisons
            .FirstOrDefaultAsync(l => l.IdLivraison == id);

        if (livraison == null)
            return NotFound("Livraison introuvable.");

        // 2. Vérifier la commande
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest(
                "La commande spécifiée n'existe pas."
            );

        // 3. Vérifier qu'une autre livraison
        // n'utilise pas déjà cette commande
        var autreLivraison = await _context.Livraisons
            .AnyAsync(l =>
                l.IdCommande == dto.IdCommande &&
                l.IdLivraison != id);

        if (autreLivraison)
            return BadRequest(
                "Cette commande possède déjà une autre livraison."
            );

        // 4. Vérifier l'adresse
        if (string.IsNullOrWhiteSpace(dto.AdresseLivraison))
            return BadRequest(
                "L'adresse de livraison est obligatoire."
            );

        // 5. Vérifier le téléphone
        if (string.IsNullOrWhiteSpace(dto.TelephoneClient))
            return BadRequest(
                "Le numéro de téléphone du client est obligatoire."
            );

        // 6. Vérifier les frais
        if (dto.FraisLivraison.HasValue &&
            dto.FraisLivraison.Value < 0)
        {
            return BadRequest(
                "Les frais de livraison ne peuvent pas être négatifs."
            );
        }

        // 7. Vérifier le statut
        var statutsAutorises = new[]
        {
            "En attente",
            "En préparation",
            "En livraison",
            "Livrée",
            "Annulée"
        };

        if (!statutsAutorises.Contains(dto.Statut))
            return BadRequest(
                "Statut invalide. Les statuts autorisés sont : " +
                "En attente, En préparation, En livraison, Livrée, Annulée."
            );

        // 8. Vérifier le livreur
        if (dto.IdLivreur.HasValue)
        {
            var livreurExiste = await _context.Livreurs
                .AnyAsync(l => l.IdLivreur == dto.IdLivreur.Value);

            if (!livreurExiste)
                return BadRequest(
                    "Le livreur spécifié n'existe pas."
                );
        }

        // 9. Mettre à jour
        livraison.IdCommande = dto.IdCommande;
        livraison.AdresseLivraison = dto.AdresseLivraison.Trim();
        livraison.TelephoneClient = dto.TelephoneClient.Trim();
        livraison.Statut = dto.Statut;
        livraison.FraisLivraison = dto.FraisLivraison;
        livraison.Remarque = dto.Remarque;
        livraison.IdLivreur = dto.IdLivreur;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Livraisons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLivraison(int id)
    {
        var livraison = await _context.Livraisons
            .FindAsync(id);

        if (livraison == null)
            return NotFound("Livraison introuvable.");

        _context.Livraisons.Remove(livraison);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}