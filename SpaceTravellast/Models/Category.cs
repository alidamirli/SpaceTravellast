using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Turlar> Turlars { get; set; } = new List<Turlar>();
}
