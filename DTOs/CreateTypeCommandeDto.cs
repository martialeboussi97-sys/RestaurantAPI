namespace RestaurantAPI.DTOs;

public class CreateTypeCommandeDto
{
    public string NomType { get; set; } = null!;

    public string? Description { get; set; }
}