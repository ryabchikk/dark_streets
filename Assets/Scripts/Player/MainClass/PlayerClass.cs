using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    public int countFighter { get; private set; }
    public int countBusiness { get; private set; }
    public TypePlayer typePlayer { get; private set; }
    public List<int> businessIndex { get; private set; }
    public Wallet Wallet { get; }

    public PlayerClass()
    {
        countFighter = 0;
        countBusiness = 0;
        typePlayer = TypePlayer.classic;
    }

    public PlayerClass(int money, TypePlayer typePlayer, Wallet wallet)
    {
        this.typePlayer = typePlayer;
        Wallet = wallet;

        businessIndex = new List<int>();
        this.countBusiness = 0;
        this.countFighter = 0;
    }

    public bool Spend(int amount)
    {
        return Wallet.TrySpendMoney(amount);
    }

    public void SetCountFighter(int countFighter)
    {
        if (countFighter >= 0)
        {
            this.countFighter = countFighter;
        }
    }

    public void SetCountBusiness(int countBusiness)
    {
        if (countBusiness >= 0)
        {
            this.countBusiness = countBusiness;
        }
    }

    public void AddNewBusinessIndex(int index)
    {
        businessIndex.Add(index);
    }
}

