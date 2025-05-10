using System;
using System.Collections.Generic;

namespace GatheringTheMagic.Models;

public partial class CardType
{
    public string? CardId { get; set; }

    public string? Type { get; set; }

    public virtual Card? Card { get; set; }
}
