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

public class Player
{
    private int money;
    private int countFighter;
    private int countBusiness;
    private TypePlayer typePlayer;

    public Player()
    {
        money = 10000;
        countFighter = 0;
        countBusiness = 0;
        typePlayer = TypePlayer.classic;
    }
    
    public Player(int money, int countFighter, int countBusiness, TypePlayer typePlayer)
    {
        this.money = money;
        this.countFighter = countFighter;
        this.countBusiness = countBusiness;
        this.typePlayer = typePlayer;
    }

    public int GetMoney() { return money; }
    public int GetCountFighter() { return countFighter; }
    public int GetCountBusiness() {  return countBusiness; }
    public TypePlayer GetTypePlayer() {  return typePlayer; }

    public void SetMoney(int money)
    { 
        if (money >= 0) { 
            this.money = money; 
        }
    }

    public void SetCountFighter(int countFighter)
    {
        if(countFighter >= 0) { 
            this.countFighter= countFighter;
        }
    }

    public void SetCountBusiness(int countBusiness) 
    {
        if (countBusiness >= 0) {
            this.countBusiness = countBusiness;
        }
    }

}
