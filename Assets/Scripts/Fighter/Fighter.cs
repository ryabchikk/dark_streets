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
    public static float DefenceCoefficient => 1.5f;

    private static Dictionary<FighterType, Fighter> _fighters = new()
    {
        [FighterType.Knuckles] = new Fighter(500, 5),
        [FighterType.Handgun] = new Fighter(1000, 10),
        [FighterType.Machinegun] = new Fighter(1500, 15)
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