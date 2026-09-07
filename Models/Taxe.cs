using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Taxe
{
    public int IdTaxe { get; set; }

    public string NomTaxe { get; set; } = null!;

    public decimal Taux { get; set; }

    public string Statut { get; set; } = null!;

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
