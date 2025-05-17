namespace ProgiTest.Server;

public class NormalCar(float basePrice) : Car(basePrice)
{
    public override CarTypes GetCarType() => CarTypes.Normal;
    
    private const float SellerNormalBaseCostPercentage = 0.02f;
    private const float BuyerNormalBaseCostPercentage = 0.1f;
    private const float BuyerNormalBaseCostMinimum = 10;
    private const float BuyerNormalBaseCostMaximum = 50;
    
    public override float GetBuyerBaseCost()
    {
        return Math.Clamp(BasePrice * BuyerNormalBaseCostPercentage,
            BuyerNormalBaseCostMinimum,
            BuyerNormalBaseCostMaximum);
    }

    public override float GetSellersSpecialCost()
    {
        return BasePrice * SellerNormalBaseCostPercentage;
    }
}