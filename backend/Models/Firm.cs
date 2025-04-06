using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Firm
    {
        [JsonPropertyName("firmName")]
        public string? FirmName { get; set; }

        [JsonPropertyName("additionalInfo")]
        public string? AdditionalInfo { get; set; }

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
}