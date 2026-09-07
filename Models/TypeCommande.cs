using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class TypeCommande
{
    public int IdTypeCommande { get; set; }

    public string NomType { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
