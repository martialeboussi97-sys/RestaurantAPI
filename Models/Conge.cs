using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Conge
{
    public int IdConge { get; set; }

    public int IdEmploye { get; set; }

    public DateOnly DateDebut { get; set; }

    public DateOnly DateFin { get; set; }

    public string TypeConge { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public string? Motif { get; set; }

    public virtual Employe IdEmployeNavigation { get; set; } = null!;
}
