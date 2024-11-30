using System.ComponentModel.DataAnnotations;

namespace ExchangeServiceAPI.DTO.Input
{
    public class ExchangeServiceInput
    {
        [Required]
        public double amount { get; set; } = 0;

        [Required]
        public string inputCurrency { get; set; } = null!;

        [Required]
        public string  outputCurrency { get; set; } = null!;
    }
}
