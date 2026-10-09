namespace RestaurantAPI.DTOs;

public class CreateUtilisateurDto
{
    public string NomUtilisateur { get; set; } = null!;
    public string MotDePasse { get; set; } = null!;
    public int IdEmploye { get; set; }
}