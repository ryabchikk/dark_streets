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
    public int money { get; private set; }
    public int countFighter { get; private set; }
    public int countBusiness { get; private set; }
    public TypePlayer typePlayer { get; private set; }
    public List<int> businessIndex { get; private set; }

    public PlayerClass()
    {
        money = 10000;
        countFighter = 0;
        countBusiness = 0;
        typePlayer = TypePlayer.classic;
    }

    public PlayerClass(int money, TypePlayer typePlayer)
    {
        this.money = money;
        this.typePlayer = typePlayer;

        businessIndex = new List<int>();
        this.countBusiness = 0;
        this.countFighter = 0;
    }

    public void SetMoney(int money)
    {
        if (money >= 0)
        {
            this.money = money;
        }
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

