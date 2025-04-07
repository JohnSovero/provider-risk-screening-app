using System.Text.Json.Serialization;

namespace backend.Models
{
    public class WorldBankResponse
    {
        [JsonPropertyName("web")]
        public string? web { get; set; }
        [JsonPropertyName("firmName")]
        public string? FirmName { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("fromDate")]
        public string? FromDate { get; set; }

        [JsonPropertyName("toDate")]
        public string? ToDate { get; set; }

        [JsonPropertyName("grounds")]
        public string? Grounds { get; set; }
    }

    public class OfacResponse{
        [JsonPropertyName("web")]
        public string? Web { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("program")]
        public string? Programs { get; set; }
        [JsonPropertyName("list")]
        public string? List { get; set; }
        [JsonPropertyName("score")]
        public string? Score { get; set; }
    }
}