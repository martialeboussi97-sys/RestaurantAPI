using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class MouvementCaisse
{
    public int IdMouvementCaisse { get; set; }

    public int IdSessionCaisse { get; set; }

    public string TypeMouvement { get; set; } = null!;

    public decimal Montant { get; set; }

    public DateTime? DateMouvement { get; set; }

    public string? Motif { get; set; }

    public virtual SessionCaisse IdSessionCaisseNavigation { get; set; } = null!;
}
