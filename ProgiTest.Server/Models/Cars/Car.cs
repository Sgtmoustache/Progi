namespace ProgiTest.Server;

public abstract class Car(float basePrice)
{
    public abstract CarTypes GetCarType();
    protected float BasePrice { get; set; } = basePrice;
    
    private const int StorageCost = 100;
    
    public abstract float GetBuyerBaseCost();
    public abstract float GetSellersSpecialCost();
    public float GetAdditionalCost()
    {
        float additionalCost = BasePrice switch
        {
            < 1 => 0,
            <= 500 and >= 1 => 5,
            <= 1000 and > 500 => 10,
            <= 3000 and > 1000 => 15,
            > 3000 => 20,
            _ => throw new ArgumentOutOfRangeException()
        };

        return additionalCost;
    }

    public float GetTotalCost() =>  BasePrice + GetAdditionalCost() + GetSellersSpecialCost() + GetBuyerBaseCost() + StorageCost;
}