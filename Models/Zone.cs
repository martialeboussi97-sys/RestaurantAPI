using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Zone
{
    public int IdZone { get; set; }

    public string NomZone { get; set; } = null!;

    public int IdSalle { get; set; }

    public virtual Salle IdSalleNavigation { get; set; } = null!;

    public virtual ICollection<TableRestaurant> TableRestaurants { get; set; } = new List<TableRestaurant>();
}
