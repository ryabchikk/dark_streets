using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FighterMarketUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI knucklesCountText;
    [SerializeField] private TextMeshProUGUI handgunCountText;
    [SerializeField] private TextMeshProUGUI machinegunCountText;
    [SerializeField] private Button buyKnucklesButton;
    [SerializeField] private Button buyHandgunButton;
    [SerializeField] private Button buyMachinegunButton;
    [SerializeField] private GameLoop gameLoop;

    private void Start()
    {
        buyKnucklesButton.onClick.AddListener(() => gameLoop.BuyFighter((int)FighterType.Knuckles));
        buyHandgunButton.onClick.AddListener(() => gameLoop.BuyFighter((int)FighterType.Handgun));
        buyMachinegunButton.onClick.AddListener(() => gameLoop.BuyFighter((int)FighterType.Machinegun));
        gameLoop.FighterMarket.AnyFighterBought += RefreshFighterCount;
    }

    private void OnDestroy()
    {
        gameLoop.FighterMarket.AnyFighterBought -= RefreshFighterCount;
    }

    private void OnEnable()
    {
        RefreshFighterCount();
    }

    private void RefreshFighterCount()
    {
        knucklesCountText.text = gameLoop.FighterMarket.GetFightersRemaining(FighterType.Knuckles).ToString();
        handgunCountText.text = gameLoop.FighterMarket.GetFightersRemaining(FighterType.Handgun).ToString();
        machinegunCountText.text = gameLoop.FighterMarket.GetFightersRemaining(FighterType.Machinegun).ToString();
    }
}
