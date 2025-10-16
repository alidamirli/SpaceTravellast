using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string? HotelName { get; set; }

    public int? HotelCityId { get; set; }

    public int? HotelCurrencyId { get; set; }

    public string? HotelPhoto { get; set; }

    public int? HotelPrice { get; set; }

    public string? HotelDescription { get; set; }

    public string? HotelMapLocation { get; set; }

    public int? HotelStars { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual City? HotelCity { get; set; }

    public virtual Currency? HotelCurrency { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
