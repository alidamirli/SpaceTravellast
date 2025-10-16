using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public string? PhotoName { get; set; }

    public int? PhotoTurlarId { get; set; }

    public int? PhotoHotelId { get; set; }

    public virtual Hotel? PhotoHotel { get; set; }

    public virtual Turlar? PhotoTurlar { get; set; }
}
