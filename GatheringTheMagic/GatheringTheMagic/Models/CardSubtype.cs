using System;
using System.Collections.Generic;

namespace GatheringTheMagic.Models;

public partial class CardSubtype
{
    public string? CardId { get; set; }

    public string? Subtype { get; set; }

    public virtual Card? Card { get; set; }
}
