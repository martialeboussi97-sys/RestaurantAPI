using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Formule
{
    public int IdFormule { get; set; }

    public string NomFormule { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Prix { get; set; }

    public bool Disponible { get; set; }

    public virtual ICollection<FormulePlat> FormulePlats { get; set; } = new List<FormulePlat>();
}
