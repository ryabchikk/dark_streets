using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public PlayerClass Owner { get; private set; }
    public int BuyPrice { get; }
    public int SellPrice => BuyPrice;  // Todo change
    public int UpgradePrice => (BuyPrice / 10) * 2;
    public int PassiveIncome {
        get
        {
            if (Owner is null)
            {
                return 0;
            }

            var sameTypeMultiplier = OwnerSameTypeBusinessesCount > 0 ? OwnerSameTypeBusinessesCount - 1 : 0;
            var nonModified = BuyPrice / 10 * sameTypeMultiplier + (Lvl != 0 ? BuyPrice / 10 + 100 * Lvl : 0);
            
            return (int)(nonModified * EventController.GetPassiveEffectFor(Type, Owner));
        }
    }

    public int PriceForCellPass => (int)((PassiveIncome / 2 + BuyPrice / 2) * EventController.GetActiveEffectFor(Type, Owner));
    public int DefendersCount => _defenders.Values.Sum();
    public int Lvl { get; private set; }
    public string Name { get; private set; }
    
    public SizeBusiness Size { get; private set; }
    public TypeBusiness Type { get; private set; }

    public event Action<BusinessClass, PlayerClass> BusinessBought;
    public event Action<BusinessClass, PlayerClass> BusinessSold;
    public event Action LevelChanged;

    private int OwnerSameTypeBusinessesCount => _getOwnerBusinessCount?.Invoke() ?? 0;

    private Dictionary<FighterType, int> _defenders = new();
    private Func<int> _getOwnerBusinessCount;

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
        
        Owner.AddFighters(FighterType.Knuckles, GetDefendersCount(FighterType.Knuckles));
        _defenders[FighterType.Knuckles] = 0;
        Owner.AddFighters(FighterType.Handgun, GetDefendersCount(FighterType.Handgun));
        _defenders[FighterType.Handgun] = 0;
        Owner.AddFighters(FighterType.Machinegun, GetDefendersCount(FighterType.Machinegun));
        _defenders[FighterType.Machinegun] = 0;
        
        Owner = null;
        Lvl = 0;
        BusinessSold?.Invoke(this, oldOwner);
    }

    public void SetOwnerSameTypeBusinessesCountCallback(Func<int> callback)
    {
        _getOwnerBusinessCount = callback;
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
        _defenders.TryAdd(type, 0);

        return _defenders[type];
    }

    public bool TryAddDefenders(FighterType type, int count)
    {
        if (DefendersCount >= 5) {
            return false;
        }
        
        if (!Owner.TrySetDefenders(type, count)) {
            return false;
        }
        
        if (_defenders.ContainsKey(type)) {
            _defenders[type] += count;
        }
        else {
            _defenders.Add(type, count);
        }

        return true;
    }

    public bool TryRemoveDefenders(FighterType type, int count)
    {
        if (GetDefendersCount(type) < count)
        {
            return false;
        }

        _defenders[type] -= count;
        
        Owner.AddFighters(type, count);
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
