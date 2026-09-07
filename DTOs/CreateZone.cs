namespace RestaurantAPI.DTOs
{
    public class CreateZoneDto
    {
        public string NomZone { get; set; } = null!;
        public int IdSalle { get; set; }
    }
}