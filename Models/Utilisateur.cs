using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public string NomUtilisateur { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public int IdEmploye { get; set; }

    public virtual Employe IdEmployeNavigation { get; set; } = null!;

    public virtual ICollection<RoleUtilisateur> IdRoles { get; set; } = new List<RoleUtilisateur>();
}
