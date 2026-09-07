namespace RestaurantAPI.DTOs
{
    public class CreatePosteDto
    {
        public string NomPoste { get; set; } = null!;
        public string? Description { get; set; }
    }
}