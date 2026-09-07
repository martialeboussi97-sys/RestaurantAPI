using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class FormulePlat
{
    public int IdFormule { get; set; }

    public int IdPlat { get; set; }

    public int Quantite { get; set; }

    public virtual Formule IdFormuleNavigation { get; set; } = null!;

    public virtual Plat IdPlatNavigation { get; set; } = null!;
}
