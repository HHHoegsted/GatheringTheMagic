using System.Text.Json.Serialization;

namespace GatheringTheMagic.Client.Models
{
    public class CardSetDTO
    {
        public string SetCode { get; set; } = string.Empty;
        public string SetName { get; set; } = string.Empty;
    }

    public class CardSetResponseData
    {
        [JsonPropertyName("$values")]
        public CardSetDTO[]? cardSets { get; set; }

    }
}
