namespace RestaurantAPI.DTOs;

public class CreatePlatDto
{
    public string NomPlat { get; set; } = null!;

    public decimal Prix { get; set; }

    public int IdCategorie { get; set; }

    public int? TempsPreparation { get; set; }
}