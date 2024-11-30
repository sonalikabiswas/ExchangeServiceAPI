using Asp.Versioning;
using ExchangeServiceAPI.DTO.Input;
using ExchangeServiceAPI.DTO.Output;
using ExchangeServiceAPI.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExchangeServiceAPI.Controllers
{

    [ApiVersion("1.0")]
    [Route("ExchangeService")]
    [ApiController]
    public class ExchangeServiceController : ControllerBase
    {
        private readonly IExchangeServiceHandler _exchangeHandler;
        private readonly ILogger<ExchangeServiceController> _logger;
        public ExchangeServiceController(IExchangeServiceHandler exchangeHandler, ILogger<ExchangeServiceController> logger)
        {
            _exchangeHandler = exchangeHandler;
            _logger = logger;
        }


        // POST api/<ExchangeServiceController>
        [HttpPost]
        public async Task<IActionResult> Post(ExchangeServiceInput payload)
        {
            if (payload == null)
                return BadRequest();
            try
            {
                ExchangeServiceOutput output = await _exchangeHandler.ConvertCurrency(payload);
                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }            
            
        }

    }
}
