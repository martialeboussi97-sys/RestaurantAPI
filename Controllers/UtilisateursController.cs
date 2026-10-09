using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilisateursController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public UtilisateursController(RestaurantDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> CreateUtilisateur(
    [FromBody] CreateUtilisateurDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NomUtilisateur) ||
            string.IsNullOrWhiteSpace(dto.MotDePasse))
        {
            return BadRequest("Le nom d'utilisateur et le mot de passe sont obligatoires.");
        }

        // Vérifier que l'employé existe
        var employe = await _context.Employes
            .Include(e => e.IdPosteNavigation)
            .FirstOrDefaultAsync(e => e.IdEmploye == dto.IdEmploye);

        if (employe == null)
        {
            return BadRequest("L'employé spécifié n'existe pas.");
        }

        // Vérifier si l'employé possède déjà un compte
        var compteEmployeExiste = await _context.Utilisateurs
            .AnyAsync(u => u.IdEmploye == dto.IdEmploye);

        if (compteEmployeExiste)
        {
            return BadRequest("Cet employé possède déjà un compte utilisateur.");
        }

        // Vérifier si le nom d'utilisateur est déjà utilisé
        var nomUtilisateurExiste = await _context.Utilisateurs
            .AnyAsync(u => u.NomUtilisateur == dto.NomUtilisateur);

        if (nomUtilisateurExiste)
        {
            return BadRequest("Ce nom d'utilisateur est déjà utilisé.");
        }

        // Créer le compte
        var utilisateur = new Utilisateur
        {
            NomUtilisateur = dto.NomUtilisateur,
            MotDePasse = dto.MotDePasse,
            IdEmploye = dto.IdEmploye
        };

        _context.Utilisateurs.Add(utilisateur);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Utilisateur créé avec succès.",
            idUtilisateur = utilisateur.IdUtilisateur,
            nomUtilisateur = utilisateur.NomUtilisateur,
            idEmploye = utilisateur.IdEmploye,
            nomEmploye = employe.Nom,
            poste = employe.IdPosteNavigation.NomPoste
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUtilisateurDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NomUtilisateur) ||
            string.IsNullOrWhiteSpace(dto.MotDePasse))
        {
            return BadRequest("Veuillez remplir tous les champs.");
        }

        var utilisateur = await _context.Utilisateurs
            .Include(u => u.IdEmployeNavigation)
            .ThenInclude(e => e.IdPosteNavigation)
            .FirstOrDefaultAsync(u =>
                u.NomUtilisateur == dto.NomUtilisateur &&
                u.MotDePasse == dto.MotDePasse);

        if (utilisateur == null)
        {
            return Unauthorized("Identifiant ou mot de passe incorrect.");
        }

        return Ok(new
        {
            message = "Connexion réussie.",
            idUtilisateur = utilisateur.IdUtilisateur,
            idEmploye = utilisateur.IdEmploye
        });
    }
}