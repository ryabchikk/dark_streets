using System.Collections.Generic;
using UnityEngine.UIElements;

public class FighterMarket
{
    private Dictionary<FighterType, int> _remainings = new()
    {
        [FighterType.Knuckles] = 10,
        [FighterType.Handgun] = 10,
        [FighterType.Machinegun] = 10
    };

    public FighterMarket()
    { }

    public bool TryBuy(FighterType type, int amount)
    {
        if (_remainings[type] < amount) 
            return false;
        
        _remainings[type] -= amount;
        return true;

    }
}
