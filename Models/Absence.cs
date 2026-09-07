using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Absence
{
    public int IdAbsence { get; set; }

    public int IdEmploye { get; set; }

    public DateOnly DateAbsence { get; set; }

    public string? Motif { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Employe IdEmployeNavigation { get; set; } = null!;
}
