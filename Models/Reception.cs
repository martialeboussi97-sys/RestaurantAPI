using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Reception
{
    public int IdReception { get; set; }

    public int IdCommandeAchat { get; set; }

    public DateTime? DateReception { get; set; }

    public string Statut { get; set; } = null!;

    public string? Remarque { get; set; }

    public virtual CommandeAchat IdCommandeAchatNavigation { get; set; } = null!;
}
