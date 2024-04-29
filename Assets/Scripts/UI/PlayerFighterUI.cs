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

    private PlayerClass _currentPlayer;

    private void Awake()
    {
        gameLoop.FighterMarket.Updated += RefreshFighterCount;
        gameLoop.TurnTransfered += RefreshFighterCount;
        gameLoop.TurnTransfered += UpdateCallbacks;
        
        //UpdateCallbacks();
    }

    private void OnDestroy()
    {
        gameLoop.FighterMarket.Updated -= RefreshFighterCount;
        gameLoop.TurnTransfered -= RefreshFighterCount;
        gameLoop.TurnTransfered -= UpdateCallbacks;
    }

    private void RefreshFighterCount()
    {
        Debug.Log("Refresh fighter count");
        knucklesCountText.text = gameLoop.PlayerModel.GetFighterCount(FighterType.Knuckles).ToString();
        handgunCountText.text = gameLoop.PlayerModel.GetFighterCount(FighterType.Handgun).ToString();
        machinegunCountText.text = gameLoop.PlayerModel.GetFighterCount(FighterType.Machinegun).ToString();
    }

    private void UpdateCallbacks()
    {
        if (_currentPlayer is not null)
        {
            _currentPlayer.FightersChanged -= RefreshFighterCount;
        }
        
        Debug.Log("Update callbacks");
        _currentPlayer = gameLoop.PlayerModel;
        _currentPlayer.FightersChanged += RefreshFighterCount;
    }
}
