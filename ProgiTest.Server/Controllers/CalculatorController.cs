using Microsoft.AspNetCore.Mvc;

namespace ProgiTest.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController(ILogger<CalculatorController> logger) : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger = logger;

    [HttpGet(Name = "GetTotalValue")]
    public ActionResult<float> Get(float basePrice, CarTypes type)
    {
        Car car = type switch
        {
            CarTypes.Normal => new NormalCar(basePrice),
            CarTypes.Deluxe => new DeluxeCar(basePrice),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        return car.GetTotalCost();
    }
    
    
    [HttpGet("car-types")]
    public ActionResult<IEnumerable<string>> GetCarTypes()
    {
        return Enum.GetNames(typeof(CarTypes)).ToList();
    }
}