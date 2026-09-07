using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class MouvementStock
{
    public int IdMouvement { get; set; }

    public string TypeMouvement { get; set; } = null!;

    public decimal Quantite { get; set; }

    public DateTime? DateMouvement { get; set; }

    public string? Motif { get; set; }

    public int? IdStock { get; set; }

    public virtual Stock? IdStockNavigation { get; set; }
}
