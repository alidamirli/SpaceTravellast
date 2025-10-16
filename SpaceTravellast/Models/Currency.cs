using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string? CurrencyName { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual ICollection<Turlar> Turlars { get; set; } = new List<Turlar>();
}
