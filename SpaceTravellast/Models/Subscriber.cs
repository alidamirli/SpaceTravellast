using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Subscriber
{
    public int SubscriberId { get; set; }

    public string? SubscriberUsername { get; set; }

    public string? SubscriberTitle { get; set; }

    public string? SubscriberEmail { get; set; }

    public string? SubscriberDescription { get; set; }
}
