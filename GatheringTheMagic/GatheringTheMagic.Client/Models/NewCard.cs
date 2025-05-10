using System.ComponentModel.DataAnnotations;

namespace GatheringTheMagic.Client.Models
{
    public class NewCard
    {
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? ManaCost { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Rarity { get; set; } = string.Empty;

        public string? Text { get; set; }

        public string? Power { get; set; }

        public string? Toughness { get; set; }
        public string? SetCode { get; set; }
        public string? SetName { get; set; }

        [Required]
        public string Artist { get; set; } = string.Empty;
    }
}
