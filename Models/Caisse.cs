using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Caisse
{
    public int IdCaisse { get; set; }

    public string NomCaisse { get; set; } = null!;

    public string? Emplacement { get; set; }

    public string Statut { get; set; } = null!;

    public virtual ICollection<SessionCaisse> SessionCaisses { get; set; } = new List<SessionCaisse>();
}
