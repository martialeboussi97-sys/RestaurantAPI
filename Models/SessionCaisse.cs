using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class SessionCaisse
{
    public int IdSessionCaisse { get; set; }

    public int IdCaisse { get; set; }

    public int IdEmploye { get; set; }

    public DateTime? DateOuverture { get; set; }

    public DateTime? DateFermeture { get; set; }

    public decimal MontantInitial { get; set; }

    public decimal? MontantFinal { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Caisse IdCaisseNavigation { get; set; } = null!;

    public virtual Employe IdEmployeNavigation { get; set; } = null!;

    public virtual ICollection<MouvementCaisse> MouvementCaisses { get; set; } = new List<MouvementCaisse>();
}
