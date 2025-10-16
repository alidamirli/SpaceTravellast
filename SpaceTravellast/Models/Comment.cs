using System;
using System.Collections.Generic;

namespace SpaceTravellast.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? CommentUsername { get; set; }

    public string? CommentTittle { get; set; }

    public string? CommentEmail { get; set; }

    public string? CommentDescription { get; set; }

    public int? CommentHotelId { get; set; }

    public int? CommentTurlarId { get; set; }

    public int? CommentNewsId { get; set; }

    public virtual Hotel? CommentHotel { get; set; }

    public virtual News? CommentNews { get; set; }

    public virtual Turlar? CommentTurlar { get; set; }
}
