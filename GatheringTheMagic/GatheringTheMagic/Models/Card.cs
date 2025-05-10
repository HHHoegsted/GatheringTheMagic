using System;
using System.Collections.Generic;
using GatheringTheMagic.Client.Models;

namespace GatheringTheMagic.Models;

public partial class Card
{
    
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? ManaCost { get; set; }

    public double Cmc { get; set; } = 0;

    public string? Type { get; set; }

    public string? Rarity { get; set; }

    public string? SetCode { get; set; }

    public string? SetName { get; set; }

    public string? Text { get; set; }

    public string? Power { get; set; }

    public string? Toughness { get; set; }

    public string? Layout { get; set; }

    public string? Number { get; set; }

    public string? Artist { get; set; }

    public string? ImageUrl { get; set; }

    public string? OriginalText { get; set; }

    public string? OriginalType { get; set; }

    public int? Multiverseid { get; set; }

    public virtual ICollection<ForeignName> ForeignNames { get; set; } = new List<ForeignName>();

    public virtual ICollection<Legality> Legalities { get; set; } = new List<Legality>();
    public virtual ICollection<CardColor> CardColors { get; set; } = new List<CardColor>();

    public Card()
    {
    }

    public Card(NewCard card)
    {
        this.Id = card.Id;
        this.Name = card.Name;
        this.ImageUrl = card.ImageUrl;
        this.ManaCost = card.ManaCost;
        this.Type = card.Type;
        this.Rarity = card.Rarity;
        this.Power = card.Power;
        this.Toughness = card.Toughness;
        this.SetCode = "HBR";
        this.SetName = "Homebrew";
        this.Artist = card.Artist;
    }
}
