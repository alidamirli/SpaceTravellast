using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceDescription { get; set; }
}
