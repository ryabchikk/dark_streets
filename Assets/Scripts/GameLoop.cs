using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private PayButton payButton;

    public event Action RoundComplete;
    public event Action TurnTransfered;
    public event Action AnyBusinessBought;
    public event Action AnyBusinessSold;
    public FighterMarket FighterMarket { get; } = new();
    public PlayerClass PlayerModel => _currentPlayer.playerClass;
    
    private Player _currentPlayer;
    private int _indexPlayer = 0;
    private int _steps = 0;
    

    private void Start()
    {
        foreach (Player player in players) 
        { 
            player.playerMovement.listNodesTransform = map.GetListNodesTransform();
        }

        for (int i = 0; i < map.GetListNodes().Count; i++)
        {
            var business = map.GetBusinessAt(i);
            if (business is not null)
            {
                business.businessClass.BusinessSold += (_, _) =>
                {
                    business.BusinessNeutralColor.material = business.DefaultMaterial;
                    ChangeChildBusinessColor(business, business.DefaultMaterial);
                    AnyBusinessSold?.Invoke();
                };
            }
        }

        _currentPlayer = players[_indexPlayer];
        walletView.SetCurrentWallet(_currentPlayer.playerClass.Wallet);
        playerNameText.text = _currentPlayer.Name;
    }

    // For debug
    public void AddMoneyToCurrentPlayer()
    {
        _currentPlayer.playerClass.Wallet.AddMoney(1000);
    }

    private void FixedUpdate()
    {
        if (_currentPlayer.playerMovement.isMoving) { 
            switchTurnButton.enabled = false;
            _currentPlayer.playerMovement.MovePlayer(ref _steps);
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
        playerNameText.text = _currentPlayer.Name;
        TurnTransfered?.Invoke();
    }

    // Callback for UI button
    public void BuyFighter(int typeNum)
    {
        var type = (FighterType)typeNum;
        if (_currentPlayer.playerClass.TryBuy(FighterMarket, type,1))
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
            .Where(business => business.Owner == _currentPlayer.playerClass);
    }

    private void StartMovingPlayer()
    {
        dice.isRolled = false;
        _steps = dice.finalSide;
        _currentPlayer.playerMovement.isMoving = true;
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

        _currentPlayer = players[_indexPlayer];
        walletView.SetCurrentWallet(_currentPlayer.playerClass.Wallet);
    }

    private void CheckZeroSteps()
    {
        if (_steps != 0) 
            return;
        
        BusinessController businessController = map.GetBusinessAt(_currentPlayer.playerMovement.currentIndex);
        if (businessController is null)
        {
            return;
        }
        
        if(businessController.businessClass.Owner is null)
        {
            card.ActivateBusinessCard(businessController.businessClass, () =>
            {
                if (businessController.businessClass.TryBuy(_currentPlayer.playerClass))
                {
                    businessController.BusinessNeutralColor.material = _currentPlayer.playerMaterial;
                    ChangeChildBusinessColor(businessController, _currentPlayer.playerMaterial);
                    card.OnSuccessfulBuy();
                    AnyBusinessBought?.Invoke();
                }
                else
                {
                    card.OnUnsuccessfulBuy();
                }
            });
        } 
        else if (businessController.businessClass.Owner != PlayerModel)
        {
            payButton.Init(PlayerModel, businessController.businessClass);
        }
    }

    private void ChangeChildBusinessColor(BusinessController businessController, Material material)
    {
        for(int i = 0; i < businessController.BusinesNeutralColorChilds.Length; i++)
        {
            businessController.BusinesNeutralColorChilds[i].material = material;
        }
    }
    
}
