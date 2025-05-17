namespace ProgiTest.Server;

public class DeluxeCar(float basePrice) : Car(basePrice)
{
    private const float SellerDeluxeBaseCostPercentage = 0.04f;
    private const float BuyerDeluxeBaseCostPercentage = 0.1f;
    private const float BuyerDeluxeBaseCostMinimum = 25;
    private const float BuyerDeluxeBaseCostMaximum = 200;

    public override CarTypes GetCarType() => CarTypes.Deluxe;

    public override float GetBuyerBaseCost()
    {
        return Math.Clamp(BasePrice * BuyerDeluxeBaseCostPercentage,
            BuyerDeluxeBaseCostMinimum,
            BuyerDeluxeBaseCostMaximum);
    }

    public override float GetSellersSpecialCost()
    {
        return BasePrice * SellerDeluxeBaseCostPercentage;
    } 
}