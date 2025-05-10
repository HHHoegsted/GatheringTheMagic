using System;
using System.Collections.Generic;

namespace GatheringTheMagic.Models;

public partial class ForeignName
{
    public int Id { get; set; }

    public string? CardId { get; set; }

    public string? Language { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Text { get; set; }

    public string? Flavor { get; set; }

    public string? ImageUrl { get; set; }

    public int? Multiverseid { get; set; }

    public virtual Card? Card { get; set; }
}
