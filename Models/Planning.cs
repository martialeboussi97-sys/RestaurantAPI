using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Planning
{
    public int IdPlanning { get; set; }

    public int IdEmploye { get; set; }

    public DateOnly DatePlanning { get; set; }

    public TimeOnly HeureDebut { get; set; }

    public TimeOnly HeureFin { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Employe IdEmployeNavigation { get; set; } = null!;
}
