using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Turlar
{
    public int TurlarId { get; set; }

    public string? TurAd { get; set; }

    public string? TurlarFirstphoto { get; set; }

    public decimal? TurPrice { get; set; }

    public string? TurDescription { get; set; }

    public DateTime? TurTarix { get; set; }

    public int? TurlarCategoryId { get; set; }

    public int? TurlarCurrencyId { get; set; }

    public int? TurlarCountryId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual Category? TurlarCategory { get; set; }

    public virtual Country? TurlarCountry { get; set; }

    public virtual Currency? TurlarCurrency { get; set; }
}
