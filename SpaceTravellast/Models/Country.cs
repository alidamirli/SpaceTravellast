using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<Turlar> Turlars { get; set; } = new List<Turlar>();
}
