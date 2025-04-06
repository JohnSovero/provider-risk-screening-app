namespace backend.Models
{    public class ProviderDto
    {
        public string? BusinessName { get; set; }
        public string? TradeName { get; set; }
        public string? TaxId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public decimal AnnualBilling { get; set; }
    }
}