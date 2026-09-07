namespace RestaurantAPI.DTOs;

public class CreateSalleDto
{
    public string NomSalle { get; set; } = null!;

    public string? Description { get; set; }
}