using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Provider{
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("businessName")]
        public string? BusinessName { get; set; }

        [JsonPropertyName("tradeName")]
        public string? TradeName { get; set; }
        
        [JsonPropertyName("taxId")]
        public string? TaxId { get; set; } // RUC (11 dígitos)

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("website")]
        public string? Website { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("annualBilling")]
        public decimal AnnualBilling { get; set; }

        [JsonPropertyName("lastEdited")]
        public DateTime LastEdited { get; set; }
    }
}