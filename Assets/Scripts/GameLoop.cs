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
    private int _prevIndex;
    

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
                    business.ChangeBusinessColors(business.DefaultLightMaterial, business.DefaultNeutralMaterial);
                    
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
        switchTurnButton.gameObject.SetActive(false);
        UpdateCurrentPlayer();
        dice.isRolled = true;
        switchTurnButton.enabled = false;
        playerNameText.text = _currentPlayer.Name;
        _prevIndex = _currentPlayer.playerMovement.currentIndex;
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
        return GetBusinessesFor(_currentPlayer.playerClass);
    }

    private IEnumerable<BusinessClass> GetBusinessesFor(PlayerClass player)
    {
        return map
            .GetListNodes()
            .Select(node => node.GetComponent<BusinessController>())
            .Where(controller => controller is not null)
            .Select(controller => controller.businessClass)
            .Where(business => business.Owner == player);
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
        
        switchTurnButton.gameObject.SetActive(true);

        if (_prevIndex > _currentPlayer.playerMovement.currentIndex)
        {
            Debug.Log("Wrap around");
            foreach (var businessClass in GetBusinessesForCurrentPlayer())
            {
                Debug.Log($"Business {businessClass.Name} passive {businessClass.PassiveIncome}");
                businessClass.Owner.Wallet.AddMoney(businessClass.PassiveIncome);
            }
        }
        
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
                    businessController.ChangeBusinessColors(_currentPlayer.playerLightMaterial, _currentPlayer.playerNeutralMaterial);
                    card.OnSuccessfulBuy();
                    
                    businessController
                        .businessClass
                        .SetOwnerSameTypeBusinessesCountCallback(
                            () => GetBusinessesFor(_currentPlayer.playerClass).Count(business => business.Type == businessController.businessClass.Type)
                            );

                    businessController.businessClass.BusinessSold += OnBusinessSold;
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
            switchTurnButton.gameObject.SetActive(false);
            payButton.Init(PlayerModel, businessController.businessClass, () => switchTurnButton.gameObject.SetActive(true));
        }
    }

    private void OnBusinessSold(BusinessClass business, PlayerClass _)
    {
        business.SetOwnerSameTypeBusinessesCountCallback(null);
    }
}
