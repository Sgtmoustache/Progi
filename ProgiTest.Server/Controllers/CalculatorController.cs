using Microsoft.AspNetCore.Mvc;
using ProgiTest.Server.Models.Cars;

namespace ProgiTest.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController(ILogger<CalculatorController> logger) : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger = logger;

    [HttpPost(Name = "GetCarCostResults")]
    public ActionResult<CarCostResponse> GetCarCostResults([FromBody] CarCostRequest? request)
    {
        if (request == null || request.CarPrice < 0 || float.IsInfinity(request.CarPrice))
        {
            return BadRequest("Invalid request data.");
        }

        if (!Enum.TryParse<CarTypes>(request.CarType, true, out var carType))
        {
            return BadRequest("Invalid car type.");
        }

        var currentCar = new Car(request.CarPrice, carType)
        {
            CarPrice = request.CarPrice,
            Type = carType
        };

        CarCostResponse response = new CarCostResponse()
        {
            BuyerBaseCost = currentCar.GetBuyerBaseCost(),
            SellerSpecialCost = currentCar.GetSellersSpecialCost(),
            AssociationCost = currentCar.GetAssociationCost(),
            StorageCost = Car.StorageCost,
            TotalCost = currentCar.GetTotalCost(),
        };

        return Ok(response);
    }

    [HttpGet("car-types")]
    public ActionResult<IEnumerable<string>> GetCarTypes()
    {
        return Enum.GetNames(typeof(CarTypes)).ToList();
    }

    public class CarCostRequest
    {
        public float CarPrice { get; set; }
        public required string CarType { get; set; }
    }

    public class CarCostResponse
    {
        public float AssociationCost { get; set; }
        public float BuyerBaseCost { get; set; }
        public float StorageCost { get; set; }
        public float SellerSpecialCost { get; set; }
        public float TotalCost { get; set; }
    }
}