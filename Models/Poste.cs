using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Poste
{
    public int IdPoste { get; set; }

    public string NomPoste { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
