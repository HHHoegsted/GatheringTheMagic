using System;
using System.Collections.Generic;

namespace GatheringTheMagic.Models;

public partial class Legality
{
    public int Id { get; set; }

    public string? CardId { get; set; }

    public string? Format { get; set; }

    public string? Legality1 { get; set; }

    public virtual Card? Card { get; set; }
}
