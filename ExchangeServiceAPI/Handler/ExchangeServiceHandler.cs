using ExchangeServiceAPI.Controllers;
using ExchangeServiceAPI.DTO.Input;
using ExchangeServiceAPI.DTO.Output;
using ExchangeServiceAPI.Models;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Text;
namespace ExchangeServiceAPI.Handler
{
    public class ExchangeServiceHandler : IExchangeServiceHandler
    {
        const string API_KEY= "3317b0ac47827c8f08ad62a0";
        const string BASE_URL = "https://v6.exchangerate-api.com/v6/";
        private readonly ILogger<ExchangeServiceHandler> _logger;

        public ExchangeServiceHandler(ILogger<ExchangeServiceHandler> logger)
        {
            _logger = logger;
        }

        public async Task<ExchangeServiceOutput> ConvertCurrency(ExchangeServiceInput input)
        {
            try
            {
                StringBuilder URLString = new StringBuilder();

                URLString.Append(BASE_URL);
                URLString.Append(API_KEY);
                URLString.Append(String.Concat("/pair/",input.inputCurrency,"/",input.outputCurrency,"/",input.amount));


                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(URLString.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseBody);
                        return new ExchangeServiceOutput
                        {
                            amount = input.amount,
                            inputCurrency = input.inputCurrency,
                            outputCurrency = input.outputCurrency,
                            value = apiResponse.conversion_result
                        };
                    }
                    else
                    {
                        _logger.LogError("Error calling API");
                        throw new HttpRequestException($"Request failed status {response.StatusCode}");
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Exception encountered while calling API");
                throw;
            }
        }
    }
}
