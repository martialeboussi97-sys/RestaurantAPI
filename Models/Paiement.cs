using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Paiement
{
    public int IdPaiement { get; set; }

    public int IdFacture { get; set; }

    public decimal MontantPaye { get; set; }

    public string ModePaiement { get; set; } = null!;

    public DateTime? DatePaiement { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Facture IdFactureNavigation { get; set; } = null!;
}
