namespace RestaurantAPI.DTOs;

public class CreateCaisseDto
{
    public string NomCaisse { get; set; } = null!;

    public string? Emplacement { get; set; }

    public string Statut { get; set; } = null!;
}