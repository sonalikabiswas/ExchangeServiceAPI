using ExchangeServiceAPI.DTO.Input;
using ExchangeServiceAPI.DTO.Output;
using System.Reflection;

namespace ExchangeServiceAPI.Handler
{
    public interface IExchangeServiceHandler
    {
        Task<ExchangeServiceOutput> ConvertCurrency(ExchangeServiceInput input);

    }
}
