using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum FighterType
{
    Knuckles = 0,
    Handgun = 1,
    Machinegun = 2
}

public class Fighter
{
    public static Fighter Knuckles => _fighters[FighterType.Knuckles];
    public static Fighter Handgun => _fighters[FighterType.Handgun];
    public static Fighter Machinegun => _fighters[FighterType.Machinegun];

    private static Dictionary<FighterType, Fighter> _fighters = new()
    {
        [FighterType.Knuckles] = new Fighter(5, 5),
        [FighterType.Handgun] = new Fighter(10, 10),
        [FighterType.Machinegun] = new Fighter(15, 15)
    };

    public int Price { get; }
    public int Power { get; }

    private Fighter(int price, int power)
    {
        Price = price;
        Power = power;
    }

    public static Fighter OfType(FighterType type)
    {
        return _fighters[type];
    }
}