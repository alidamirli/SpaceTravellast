using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public int? UserStatusId { get; set; }

    public string? UserPassword { get; set; }

    public string? UserEmail { get; set; }

    public virtual Status? UserStatus { get; set; }
}
