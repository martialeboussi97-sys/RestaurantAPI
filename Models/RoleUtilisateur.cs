using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class RoleUtilisateur
{
    public int IdRole { get; set; }

    public string NomRole { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Utilisateur> IdUtilisateurs { get; set; } = new List<Utilisateur>();
}
