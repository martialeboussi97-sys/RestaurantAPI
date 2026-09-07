using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class OptionPlat
{
    public int IdOption { get; set; }

    public string NomOption { get; set; } = null!;

    public string? Description { get; set; }

    public decimal PrixSupplement { get; set; }

    public bool Disponible { get; set; }

    public virtual ICollection<LigneCommandeOption> LigneCommandeOptions { get; set; } = new List<LigneCommandeOption>();

    public virtual ICollection<Plat> IdPlats { get; set; } = new List<Plat>();
}
