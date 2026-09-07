using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Presence
{
    public int IdPresence { get; set; }

    public int IdEmploye { get; set; }

    public DateOnly DatePresence { get; set; }

    public TimeOnly? HeureArrivee { get; set; }

    public TimeOnly? HeureDepart { get; set; }

    public string Statut { get; set; } = null!;

    public string? Remarque { get; set; }

    public virtual Employe IdEmployeNavigation { get; set; } = null!;
}
