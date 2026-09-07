using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Categorie
{
    public int IdCategorie { get; set; }

    public string NomCategorie { get; set; } = null!;

    public virtual ICollection<Plat> Plats { get; set; } = new List<Plat>();
}
