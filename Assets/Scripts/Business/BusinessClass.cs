using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBusiness
{
    //Alcohol, Weapon, Casino, Tobacco, Tote
    Алкоголь, Оружие, Казино, Табак, Тотализатор
}
public enum SizeBusiness
{
    //Small, Medium, Big
    Малый, Средний, Большой
}
public class BusinessClass
{   
    public int price { get; private set; }
    public int pumpingPrice { get; private set; }
    public int passiveIncome { get; private set; }
    public int priceForCellPass { get; private set; }
    public SizeBusiness size { get; private set; }
    public TypeBusiness type { get; private set; }
    public string name { get; private set; }
    public int lvl { get; private set; }
    public PlayerClass Owner { get; private set; }
    public event Action<BusinessClass, PlayerClass> BusinessBought;
    public event Action<BusinessClass, PlayerClass> BusinessSold; 

    public BusinessClass(int price, string name, SizeBusiness size, TypeBusiness type)
    {
        this.price = price;
        this.size = size;
        this.type = type;
        this.name = name;
        this.lvl = 0;

        SetPassiveIncome();
        SetPumpingPrice();
        SetPriceForCellPass();
    }

    public void SetPassiveIncome() { passiveIncome = CalculatePassiveIncome(); }
    public void SetPumpingPrice() {  pumpingPrice = CalculatePumpingPrice(); }
    public void SetPriceForCellPass() { priceForCellPass = CalculatePriceForCellPass(); }

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
        if (!player.Wallet.TrySpendMoney(price))
            return false;
        
        SetOwner(player);
        BusinessBought?.Invoke(this, player);
        return true;
    }

    public void Sell()
    {
        Owner.Wallet.AddMoney(price);
        var oldOwner = Owner;
        Owner = null;
        BusinessSold?.Invoke(this, oldOwner);
    }

    public bool TryUpgrade()
    {
        if (!Owner.Wallet.TrySpendMoney(pumpingPrice))
            return false;
        if (lvl >= 5)
            return false;
        
        ++lvl;
        return true;
    }

    public bool TryDowngrade()
    {
        if (lvl <= 1)
            return false;
        
        Owner.Wallet.AddMoney(pumpingPrice);
        return true;
    }

    public bool IsAvailableToUpgrade()
    {
        return lvl < 5;
    }

    public bool IsAvailableToDowngrade()
    {
        return lvl >= 1;
    }

    public bool IsAvailableToBuy()
    {
        return Owner is null;
    }
    
    private int CalculatePassiveIncome() => lvl != 0 ? (price / 10) + (100 * lvl) : 0;
    private int CalculatePumpingPrice() => (price / 10) * 2;
    private int CalculatePriceForCellPass() => passiveIncome / 2 + price / 2;
}
