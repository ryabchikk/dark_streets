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
    public event Action Updated;
    public event Action<PlayerClass> Captured; 

    private int OwnerSameTypeBusinessesCount => _getOwnerBusinessCount?.Invoke(Owner) ?? 0;

    private Dictionary<FighterType, int> _defenders = new();
    private Func<PlayerClass, int> _getOwnerBusinessCount;

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
            Updated?.Invoke();
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
        Updated?.Invoke();
        return true;
    }

    public void NotifyNewEvent(Event e)
    {
        if (e.Type == EventType.Global && e.AffectedBusinessType == Type)
        {
            Updated?.Invoke();
        }
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
        Updated?.Invoke();
    }

    public void SetOwnerSameTypeBusinessesCountCallback(Func<PlayerClass, int> callback)
    {
        _getOwnerBusinessCount = callback;
    }

    public bool Attack(PlayerClass attacker, Dictionary<FighterType, int> fighters)
    {
        double attackPower = fighters[FighterType.Knuckles] * Fighter.Knuckles.Power +
                             fighters[FighterType.Handgun] * Fighter.Handgun.Power +
                             fighters[FighterType.Machinegun] * Fighter.Machinegun.Power;
        
        double defencePower = GetDefendersCount(FighterType.Knuckles) * Fighter.Knuckles.Power +
                              GetDefendersCount(FighterType.Handgun) * Fighter.Handgun.Power +
                              GetDefendersCount(FighterType.Machinegun) * Fighter.Machinegun.Power;
        defencePower = Math.Ceiling(defencePower * Fighter.DefenceCoefficient);
        
        attacker.RemoveFighters(FighterType.Knuckles, fighters[FighterType.Knuckles]);
        attacker.RemoveFighters(FighterType.Handgun, fighters[FighterType.Handgun]);
        attacker.RemoveFighters(FighterType.Machinegun, fighters[FighterType.Machinegun]);
        
        Func<FighterType, int> getFighterCount;
        var result = attackPower - defencePower;
        
        if (result > 0)
        {
            getFighterCount = type => fighters[type];
        }
        else
        {
            result /= Fighter.DefenceCoefficient;
            getFighterCount = GetDefendersCount;
        }
        
        var remaining = DistributeRemainingFighters(Math.Abs(result), getFighterCount);
        
        if (result > 0)
        {
            attacker.AddFighters(FighterType.Knuckles, remaining[FighterType.Knuckles]);
            attacker.AddFighters(FighterType.Handgun, remaining[FighterType.Handgun]);
            attacker.AddFighters(FighterType.Machinegun, remaining[FighterType.Machinegun]);
            Capture(attacker);
        }
        else
        {
            _defenders[FighterType.Knuckles] = remaining[FighterType.Knuckles];
            _defenders[FighterType.Handgun] = remaining[FighterType.Handgun];
            _defenders[FighterType.Machinegun] = remaining[FighterType.Machinegun];
        }
        
        Updated?.Invoke();

        return result > 0;
    }

    public bool TryUpgrade()
    {
        if (!IsAvailableToUpgrade())
            return false;
        if (!Owner.Wallet.TrySpendMoney(UpgradePrice))
            return false;

        ++Lvl;
        LevelChanged?.Invoke();
        Updated?.Invoke();
        return true;
    }

    public bool TryDowngrade()
    {
        if (!IsAvailableToDowngrade())
            return false;
        
        Owner.Wallet.AddMoney(UpgradePrice);
        --Lvl;
        LevelChanged?.Invoke();
        Updated?.Invoke();
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
        
        Debug.Log("Try add defenders");
        if (!Owner.TrySetDefenders(type, count)) {
            return false;
        }
        
        if (_defenders.ContainsKey(type)) {
            _defenders[type] += count;
        }
        else {
            _defenders.Add(type, count);
        }

        Updated?.Invoke();
        return true;
    }

    private void Capture(PlayerClass newOwner)
    {
        _defenders[FighterType.Knuckles] = 0;
        _defenders[FighterType.Handgun] = 0;
        _defenders[FighterType.Machinegun] = 0;

        Owner = newOwner;
        Captured?.Invoke(Owner);
    }

    public bool TryRemoveDefenders(FighterType type, int count)
    {
        if (GetDefendersCount(type) < count)
        {
            return false;
        }

        _defenders[type] -= count;
        
        Owner.AddFighters(type, count);
        Updated?.Invoke();
        return true;
    }
    
    private int GetRemainingFighters(ref double remainingPower, Fighter fighter, int notMoreThan)
    {
        int result = 0;
        Debug.Log($"rem {remainingPower} power {fighter.Power} not nore than {notMoreThan}");
        while (result < notMoreThan && remainingPower >= fighter.Power)
        {
            remainingPower -= fighter.Power;
            result++;
            Debug.Log($"rem {remainingPower} power {fighter.Power} not nore than {notMoreThan} result {result}");
        }

        if (result < notMoreThan && remainingPower >= fighter.Power * 0.5)
        {
            remainingPower = 0;
            result++;
        }

        return result;
    }

    private Dictionary<FighterType, int> DistributeRemainingFighters(double result, Func<FighterType, int> getFighterCount)
    {
        var dict = new Dictionary<FighterType, int>();
        dict.Add(FighterType.Machinegun, GetRemainingFighters(ref result, Fighter.Machinegun, getFighterCount(FighterType.Knuckles)));
        dict.Add(FighterType.Handgun, GetRemainingFighters(ref result, Fighter.Handgun, getFighterCount(FighterType.Handgun)));
        dict.Add(FighterType.Knuckles, GetRemainingFighters(ref result, Fighter.Knuckles, getFighterCount(FighterType.Machinegun)));
        
        var market = FighterMarket.Instance;
        market.ReturnFighters(FighterType.Knuckles, getFighterCount(FighterType.Knuckles) - dict[FighterType.Knuckles]);
        market.ReturnFighters(FighterType.Handgun, getFighterCount(FighterType.Handgun) - dict[FighterType.Handgun]);
        market.ReturnFighters(FighterType.Machinegun, getFighterCount(FighterType.Machinegun) - dict[FighterType.Machinegun]);

        return dict;
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
