using AutoMapper;
using ExchangeServiceAPI.Controllers;
using ExchangeServiceAPI.DTO.Input;
using ExchangeServiceAPI.DTO.Output;
using ExchangeServiceAPI.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExchangeServiceAPI.Test
{
    public class ExchangeServiceController_UnitTests
    {
        private readonly Mock<IExchangeServiceHandler> serviceHandler;
        private readonly Mock<ILogger<ExchangeServiceController>> logger;
        private readonly ExchangeServiceController controller;
        public ExchangeServiceController_UnitTests()
        {
            serviceHandler = new Mock<IExchangeServiceHandler>();
            logger = new Mock<ILogger<ExchangeServiceController>>();
            controller = new ExchangeServiceController(serviceHandler.Object, logger.Object);

        }

        [Fact]
        public async Task Post_WhenSuccess_ReturnsOKResult()
        {
            // Arrange
            var payload = new ExchangeServiceInput { amount=50, inputCurrency="AUD", outputCurrency="USD" };
            var expectedOutput = new ExchangeServiceOutput { amount = 50, inputCurrency = "AUD", outputCurrency = "USD" , value= 32.545 };
            serviceHandler.Setup(x => x.ConvertCurrency(payload)).ReturnsAsync(expectedOutput);

            // Act
            var result = await controller.Post(payload);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = actionResult.Value as ExchangeServiceOutput;
            Assert.Equal(expectedOutput, returnValue);
        }

        [Fact]
        public async Task Post_WhenHandlerThrowsException_Returns500Status()
        {
            // Arrange
            var payload = new ExchangeServiceInput { amount = 50, inputCurrency = "Invalid", outputCurrency = "Invalid" };
            serviceHandler.Setup(x => x.ConvertCurrency(payload)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await controller.Post(payload);

            // Assert
            var actionResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, actionResult.StatusCode);
        }
    }
}