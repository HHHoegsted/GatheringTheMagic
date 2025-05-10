using System;
using System.Collections.Generic;

namespace GatheringTheMagic.Models;

public partial class CardColor
{
    public string? CardId { get; set; }

    public string? Color { get; set; }

    public virtual Card? Card { get; set; }
}
