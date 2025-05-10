using System;
using System.Collections.Generic;

namespace GatheringTheMagic.Models;

public partial class CardPrinting
{
    public string? CardId { get; set; }

    public string? SetCode { get; set; }

    public virtual Card? Card { get; set; }
}
