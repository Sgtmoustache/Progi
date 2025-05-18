using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProgiTest.Server.Controllers;
using ProgiTest.Server.Models.Cars;

namespace ProgiTest.Server.Tests
{
    public class CalculatorControllerTests
    {
        private readonly CalculatorController _controller;

        public CalculatorControllerTests()
        {
            var loggerMock = new Mock<ILogger<CalculatorController>>();
            _controller = new CalculatorController(loggerMock.Object);
        }

        [Theory]
        [InlineData(398.00f, CarTypes.Normal, 39.80f, 7.96f, 5.00f, 550.76f)]
        [InlineData(1800.00f, CarTypes.Deluxe, 180.00f, 72.00f, 15.00f, 2167.00f)]
        public void GetCarCostResults_ShouldReturnCarCostResponse_Test(float carPrice, CarTypes type, float expectedBaseCost,
            float expectedSpecialCost, float expectedAssociation, float expectedTotalCost)
        {
            var actionResult = _controller.GetCarCostResults(
                new CalculatorController.CarCostRequest()
                {
                    CarPrice = carPrice,
                    CarType = type.ToString(),
                });

            Assert.IsType<OkObjectResult>(actionResult.Result);
            OkObjectResult result = (OkObjectResult) actionResult.Result;
            Assert.NotNull(result);
            var response = Assert.IsType<CalculatorController.CarCostResponse>(result.Value);
            Assert.Equal(expectedBaseCost, response.BuyerBaseCost);
            Assert.Equal(expectedSpecialCost, response.SellerSpecialCost);
            Assert.Equal(expectedAssociation, response.AssociationCost);
            Assert.Equal(expectedTotalCost, response.TotalCost);
        }

        [Fact]
        public void GetCarTypes_ShouldReturnListOfCarTypes_Test()
        {
            var result = _controller.GetCarTypes();

            Assert.NotNull(result);
            var carTypes = Assert.IsType<List<string>>(result.Value);
            Assert.Contains(nameof(CarTypes.Normal), carTypes);
            Assert.Contains(nameof(CarTypes.Deluxe), carTypes);
            Assert.Equal(2, carTypes.Count);
        }
    }
}