using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class FighterMarket
{
    // Type of fighter been bought, amount been bought, amount remaining
    public event Action<FighterType, int, int> FighterBought;
    
    private Dictionary<FighterType, int> _remainings = new()
    {
        [FighterType.Knuckles] = 10,
        [FighterType.Handgun] = 10,
        [FighterType.Machinegun] = 10
    };

    public bool TryBuy(FighterType type, int amount)
    {
        if (_remainings[type] < amount) 
            return false;
        
        _remainings[type] -= amount;
        FighterBought?.Invoke(type, amount, GetFightersRemaining(type));
        return true;
    }

    public int GetFightersRemaining(FighterType type)
    {
        return _remainings[type];
    }
}
