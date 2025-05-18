namespace ProgiTest.Server.Models.Cars;

public class Car(float carPrice, CarTypes type)
{
    #region Normal

    private const float SellerNormalBaseCostPercentage = 0.02f;
    private const float BuyerNormalBaseCostPercentage = 0.1f;
    private const float BuyerNormalBaseCostMinimum = 10;
    private const float BuyerNormalBaseCostMaximum = 50;

    #endregion

    #region Deluxe

    private const float SellerDeluxeBaseCostPercentage = 0.04f;
    private const float BuyerDeluxeBaseCostPercentage = 0.1f;
    private const float BuyerDeluxeBaseCostMinimum = 25;
    private const float BuyerDeluxeBaseCostMaximum = 200;

    #endregion

    #region Shared

    public const int StorageCost = 100;

    #endregion

    public CarTypes Type = type;
    public float CarPrice { get; set; } = carPrice;

    public float GetBuyerBaseCost()
    {
        var total = Type switch
        {
            CarTypes.Normal => Math.Clamp(CarPrice * BuyerNormalBaseCostPercentage, BuyerNormalBaseCostMinimum,
                BuyerNormalBaseCostMaximum),
            CarTypes.Deluxe => Math.Clamp(CarPrice * BuyerDeluxeBaseCostPercentage, BuyerDeluxeBaseCostMinimum,
                BuyerDeluxeBaseCostMaximum),
            _ => throw new ArgumentOutOfRangeException()
        };

        return (float)Math.Round(total, 2);
    }

    public float GetSellersSpecialCost()
    {
        var total = Type switch
        {
            CarTypes.Normal => CarPrice * SellerNormalBaseCostPercentage,
            CarTypes.Deluxe => CarPrice * SellerDeluxeBaseCostPercentage,
            _ => throw new ArgumentOutOfRangeException()
        };

        return (float)Math.Round(total, 2);
    }

    public float GetAssociationCost()
    {
        float additionalCost = CarPrice switch
        {
            < 1 => 0,
            <= 500 and >= 1 => 5,
            <= 1000 and > 500 => 10,
            <= 3000 and > 1000 => 15,
            > 3000 => 20,
            _ => throw new ArgumentOutOfRangeException()
        };

        return (float)Math.Round(additionalCost, 2);
    }

    public float GetTotalCost() =>
        (float)Math.Round(CarPrice + GetBuyerBaseCost() + GetSellersSpecialCost() + GetAssociationCost() + +StorageCost,
            2);
}