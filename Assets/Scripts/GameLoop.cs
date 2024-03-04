using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private Player[] players;
    [SerializeField] private Map map;
    [SerializeField] private Dice dice;
    [SerializeField] private BusinessCard card;
    [SerializeField] private WalletView walletView;
    [SerializeField] private Button switchTurnButton;

    public event Action RoundComplete;
    
    private Player currentPlayer;
    private int _indexPlayer = 0;
    private int _steps = 0;
    private FighterMarket _fighterMarket = new();

    private void Start()
    {
        foreach (Player player in players) 
        { 
            player.playerMovement.listNodesTransform = map.GetListNodesTransform();
        }

        currentPlayer = players[_indexPlayer];
        walletView.SetCurrentWallet(currentPlayer.playerClass.Wallet);
    }

    // For debug
    public void AddMoneyToCurrentPlayer()
    {
        currentPlayer.playerClass.Wallet.AddMoney(1000);
    }

    private void FixedUpdate()
    {
        if (currentPlayer.playerMovement.isMoving) { 
            currentPlayer.playerMovement.MovePlayer(ref _steps);
            CheckZeroSteps();
        }
        else {
            //dice.isRolled = true;
            switchTurnButton.enabled = true;
        }
    }
    
    private void OnEnable()
    {
        dice.DiceRolled += StartMovingPlayer; // Change to StartMovingPlayer when turn transfer mechanic is ready
    }

    private void OnDisable()
    {
        dice.DiceRolled -= StartMovingPlayer; // Change to StartMovingPlayer when turn transfer mechanic is ready
    }

    public void SwitchTurn()
    {
        UpdateCurrentPlayer();
        dice.isRolled = true;
        switchTurnButton.enabled = false;
    }

    // Callback for UI button
    public void BuyFighter(int typeNum)
    {
        var type = (FighterType)typeNum;
        if (currentPlayer.playerClass.TryBuy(_fighterMarket, type,1))
        {
            // todo
        }
        else
        {
            // todo
        }
    }

    public IEnumerable<BusinessClass> GetBusinessesForCurrentPlayer()
    {
        return map
            .GetListNodes()
            .Select(node => node.GetComponent<BusinessController>())
            .Where(controller => controller is not null)
            .Select(controller => controller.businessClass)
            .Where(business => business.Owner == currentPlayer.playerClass);
    }

    private void StartMovingPlayer()
    {
        dice.isRolled = false;
        _steps = dice.finalSide;
        currentPlayer.playerMovement.isMoving = true;
        //currentPlayer.playerClass.Spend(1000);
    }

    private void UpdateCurrentPlayer()
    {
        if(_indexPlayer<players.Length - 1) {
            _indexPlayer++; 
        }
        else {
            _indexPlayer = 0;
            RoundComplete?.Invoke();
        }

        currentPlayer = players[_indexPlayer];
        walletView.SetCurrentWallet(currentPlayer.playerClass.Wallet);
    }

    private void CheckZeroSteps()
    {
        if (_steps != 0) 
            return;
        
        var businessController = map.GetBusinessAt(currentPlayer.playerMovement.currentIndex);
        if (businessController is null)
        {
            return;
        }
        
        if(businessController.businessClass.Owner is null)
        {
            card.ActivateBusinessCard(businessController.businessClass, () =>
            {
                if (businessController.businessClass.TryBuy(currentPlayer.playerClass))
                {
                    card.OnSuccessfulBuy();
                }
                else
                {
                    card.OnUnsuccessfulBuy();
                }
            });
        }
    }
}
