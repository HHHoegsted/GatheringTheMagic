using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GatheringTheMagic.Client.Models
{
    public class CardOverview
    {
        [Key]
        public string ID { get; set; }
        public string? name { get; set; }
        public string? manaCost { get; set; }
        public string? power { get; set; }
        public string? toughness { get; set; }
        public string? type { get; set; }

        [JsonPropertyName("cardColors")]
        public Colors? CardColors { get; set; }
    }

    public class Colors
    {
        [JsonPropertyName("$values")]
        public Colorvalues[]? Colorvalue { get; set; }
    }

    public class Colorvalues
    {
        [JsonPropertyName("color")]
        public string? color {  get; set; }
    }
}
