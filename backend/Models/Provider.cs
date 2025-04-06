using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Provider{
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        [StringLength(100, ErrorMessage = "La razón social no puede exceder los 100 caracteres.")]
        [JsonPropertyName("businessName")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "El nombre comercial es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre comercial no puede exceder los 100 caracteres.")]
        [JsonPropertyName("tradeName")]
        public string? TradeName { get; set; }

        [Required(ErrorMessage = "La identificación tributaria es obligatoria.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La identificación tributaria debe tener 11 dígitos.")]
        [JsonPropertyName("taxId")]
        public string? TaxId { get; set; } // RUC (11 dígitos)

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Url(ErrorMessage = "La URL proporcionada no es válida.")]
        [JsonPropertyName("website")]
        public string? Website { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        [StringLength(50, ErrorMessage = "El país no puede exceder los 50 caracteres.")]
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La facturación anual debe ser un número positivo.")]
        [JsonPropertyName("annualBilling")]
        public decimal AnnualBilling { get; set; }

        [Required(ErrorMessage = "La fecha de última edición es obligatoria.")]
        [JsonPropertyName("lastEdited")]
        public DateTime LastEdited { get; set; }
    }
}