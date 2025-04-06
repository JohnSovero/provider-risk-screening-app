namespace backend.Models
{    public class ProviderDto
    {
        public required string BusinessName { get; set; }
        public required string TradeName { get; set; }
        public required string TaxId { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Website { get; set; }
        public required string Address { get; set; }
        public required string Country { get; set; }
        public required decimal AnnualBilling { get; set; }
    }
}