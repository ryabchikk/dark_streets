using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum TypePlayer
{
    classic,
    fither,
    money,
    buisness
}

public class PlayerClass
{
    public int money => Wallet.money;
    public int CountFighter => _fighters.Values.Sum();
    public int countBusiness { get; private set; }
    public TypePlayer typePlayer { get; private set; }
    public Wallet Wallet { get; }
    public event Action FightersChanged; 

    private Dictionary<FighterType, int> _fighters;

    public PlayerClass(int money, TypePlayer typePlayer, Wallet wallet)
    {
        this.typePlayer = typePlayer;
        Wallet = wallet;
        Wallet.AddMoney(money);

        countBusiness = 0;
        _fighters = new Dictionary<FighterType, int>();
    }

    public int GetFighterCount(FighterType type)
    {
        return _fighters.TryGetValue(type, out var count) ? count : 0;
    }

    public bool TryBuy(FighterMarket market, FighterType type, int amount)
    {
        var price = Fighter.OfType(type).Price;
        if (Wallet.money < price * amount || market.GetFightersRemaining(type) < amount)
            return false;
        
        Wallet.TrySpendMoney(price * amount);
        AddFighters(type, amount);
        market.TryBuy(type, amount);
        
        return true;
    }

    public void AddFighters(FighterType type, int count)
    {
        if (_fighters.ContainsKey(type))
        {
            _fighters[type] += count;
        }
        else
        {
            _fighters.Add(type, count);
        }
        FightersChanged?.Invoke();
    }

    public void RemoveFighters(FighterType type, int count)
    {
        if (_fighters.ContainsKey(type))
        {
            _fighters[type] -= Math.Min(count, _fighters[type]);
        }
        
        FightersChanged?.Invoke();
    }

    public bool TrySetDefenders(FighterType type, int count)
    {
        if (GetFighterCount(type) < count)
        {
            return false;
        }

        _fighters[type] -= count;
        FightersChanged?.Invoke();
        return true;
    }
}

