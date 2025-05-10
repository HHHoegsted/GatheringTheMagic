using System.Text.Json.Serialization;

namespace GatheringTheMagic.Client.Models
{
    public class PaginatedResponse
    {
        [JsonPropertyName("data")]
        public ResponseData Data { get; set; } = new();
        
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalRecords")]
        public int TotalItems { get; set; }
    }

    public class ResponseData()
    {
        [JsonPropertyName("$values")]
        public CardOverview[]? Cards { get; set; }

    }
}
