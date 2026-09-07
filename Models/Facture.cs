using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Facture
{
    public int IdFacture { get; set; }

    public int IdCommande { get; set; }

    public DateTime? DateFacture { get; set; }

    public decimal MontantTotal { get; set; }

    public decimal? Remise { get; set; }

    public decimal MontantNet { get; set; }

    public int? IdTaxe { get; set; }

    public virtual Commande IdCommandeNavigation { get; set; } = null!;

    public virtual Taxe? IdTaxeNavigation { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
