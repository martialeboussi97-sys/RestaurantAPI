namespace RestaurantAPI.DTOs
{
    public class CreateEmployeDto
    {
        public string NumeroEmploye { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string? Prenom { get; set; }
        public int IdPoste { get; set; }
    }
}