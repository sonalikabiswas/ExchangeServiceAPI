using System.ComponentModel.DataAnnotations;

namespace ExchangeServiceAPI.DTO.Output
{
    public class ExchangeServiceOutput
    {
        public double amount { get; set; } = 0;
        public string inputCurrency { get; set; } = null!;
        public string outputCurrency { get; set; } = null!;
        public double value { get; set; } = 0;
    }
}
