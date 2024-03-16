using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerFighterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI knucklesCountText;
    [SerializeField] private TextMeshProUGUI handgunCountText;
    [SerializeField] private TextMeshProUGUI machinegunCountText;
    [SerializeField] private GameLoop gameLoop;

    private void Awake()
    {
        gameLoop.FighterMarket.AnyFighterBought += RefreshFighterCount;
        gameLoop.TurnTransfered += RefreshFighterCount;
    }

    private void OnDestroy()
    {
        gameLoop.FighterMarket.AnyFighterBought -= RefreshFighterCount;
        gameLoop.TurnTransfered -= RefreshFighterCount;
    }

    private void RefreshFighterCount()
    {
        knucklesCountText.text = gameLoop.PlayerModel.GetFighterCount(FighterType.Knuckles).ToString();
        handgunCountText.text = gameLoop.PlayerModel.GetFighterCount(FighterType.Handgun).ToString();
        machinegunCountText.text = gameLoop.PlayerModel.GetFighterCount(FighterType.Machinegun).ToString();
    }
}
