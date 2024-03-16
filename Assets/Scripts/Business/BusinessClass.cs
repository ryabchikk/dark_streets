using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBusiness
{
    //Alcohol, Weapon, Casino, Tobacco, Tote
    Алкоголь = 0, Оружие = 1, Казино = 2, Табак = 3, Тотализатор =4
}
public enum SizeBusiness
{
    //Small, Medium, Big
    Малый = 0, Средний = 1, Большой = 2
}
public class BusinessClass
{   
    public int BuyPrice { get; }
    public int SellPrice => BuyPrice;  // Todo change
    public int UpgradePrice => (BuyPrice / 10) * 2;
    public int PassiveIncome => Lvl != 0 ? BuyPrice / 10 + 100 * Lvl : 0;
    public int PriceForCellPass => PassiveIncome / 2 + BuyPrice / 2;
    public SizeBusiness Size { get; private set; }
    public TypeBusiness Type { get; private set; }
    public string Name { get; private set; }
    public int Lvl { get; private set; }
    public PlayerClass Owner { get; private set; }
    public event Action<BusinessClass, PlayerClass> BusinessBought;
    public event Action<BusinessClass, PlayerClass> BusinessSold;
    public event Action LevelChanged;

    private Dictionary<FighterType, int> _defenders;

    public BusinessClass(int buyPrice, string name, SizeBusiness size, TypeBusiness type)
    {
        BuyPrice = buyPrice;
        Size = size;
        Type = type;
        Name = name;
        Lvl = 0;
    }

    public bool SetOwner(PlayerClass player)
    {
        if (player is not null && Owner is null)
        {
            Owner = player;
            return true;
        }

        return false;
    }

    public bool TryBuy(PlayerClass player)
    {
        if (!IsAvailableToBuy())
        {
            return false;
        }
        
        if (!player.Wallet.TrySpendMoney(BuyPrice))
        {
            return false;
        }
        
        SetOwner(player);
        BusinessBought?.Invoke(this, player);
        return true;
    }

    public void Sell()
    {
        Owner.Wallet.AddMoney(BuyPrice);
        var oldOwner = Owner;
        Owner = null;
        Lvl = 0;
        BusinessSold?.Invoke(this, oldOwner);
    }

    public bool TryUpgrade()
    {
        if (!IsAvailableToUpgrade())
            return false;
        if (!Owner.Wallet.TrySpendMoney(UpgradePrice))
            return false;

        ++Lvl;
        LevelChanged?.Invoke();
        return true;
    }

    public bool TryDowngrade()
    {
        if (!IsAvailableToDowngrade())
            return false;
        
        Owner.Wallet.AddMoney(UpgradePrice);
        --Lvl;
        LevelChanged?.Invoke();
        return true;
    }
    
    public int GetDefendersCount(FighterType type)
    {
        return _defenders.TryGetValue(type, out var count) ? count : 0;
    }

    public bool TryAddDefenders(FighterType type, int count)
    {
        if (Owner.TrySetDefenders(type, count))
        {
            return false;
        }
        
        if (_defenders.ContainsKey(type))
        {
            _defenders[type] += count;
        }
        else
        {
            _defenders.Add(type, count);
        }

        return true;
    }

    private bool IsAvailableToUpgrade()
    {
        return Lvl < 5;
    }

    private bool IsAvailableToDowngrade()
    {
        return Lvl > 0;
    }

    private bool IsAvailableToBuy()
    {
        return Owner is null;
    }
}
