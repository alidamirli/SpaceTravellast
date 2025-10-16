using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string? NewsName { get; set; }

    public string? NewsDescription { get; set; }

    public string? NewsPhoto { get; set; }

    public DateTime? NewsTime { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
