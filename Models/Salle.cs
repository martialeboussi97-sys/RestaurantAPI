using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Salle
{
    public int IdSalle { get; set; }

    public string NomSalle { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Zone> Zones { get; set; } = new List<Zone>();
}
