using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

    private Dictionary<FighterType, int> _fighters;

    public PlayerClass(int money, TypePlayer typePlayer, Wallet wallet)
    {
        this.typePlayer = typePlayer;
        Wallet = wallet;

        countBusiness = 0;
        _fighters = new Dictionary<FighterType, int>();
    }

    public int GetFighterCount(FighterType type)
    {
        return _fighters[type];
    }
}

