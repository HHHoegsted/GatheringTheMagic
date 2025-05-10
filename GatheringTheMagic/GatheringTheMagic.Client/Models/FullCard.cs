namespace GatheringTheMagic.Client.Models
{
    public class FullCard
    {
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? ManaCost { get; set; }

        public double Cmc { get; set; }

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
    }
}
