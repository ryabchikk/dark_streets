using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EventType
{
    Local, Global
}

public enum AffectedPlayerStat
{
    Fighters, Money
}

[CreateAssetMenu(menuName = "New event")]
public class Event : ScriptableObject
{
    public EventType Type;
    public int Duration;
    
    public string Name;
    public string Description;
    public string EffectDescription;
    
    [Header("For global events")]
    public TypeBusiness AffectedBusinessType;
    // Set to 1 for no effect
    public float PassiveIncomeModifier;
    public float ActiveIncomeModifier;

    [Header("For local events")]
    public AffectedPlayerStat AffectedPlayerStat;
    public int Dice;  // 4 for 1d4, 6 for 1d6 etc
    // Formula is DiceConstant + DiceMultiplier * RolledDice
    public int DiceMultiplier;
    public int DiceConstant;

    public int GetDiceEffect()
    {
        return DiceConstant + Random.Range(1, Dice + 1) * DiceMultiplier;
    }
}