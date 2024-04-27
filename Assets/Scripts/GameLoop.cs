using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UIAnimations;
using Random = UnityEngine.Random;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private List<Player> players;
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Map map;
    [SerializeField] private Dice dice;
    [SerializeField] private BusinessCard card;
    [SerializeField] private WalletView walletView;
    [SerializeField] private Button switchTurnButton;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private PayButton payButton;
    [SerializeField] private EventCard eventCard;

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
        for (int i = 0; i < GameCreation.PlayersCount; ++i)
        {
            var player = playerPrefabs[i];
            var go = Instantiate(player, new Vector3(1.63f, 0, -72.44f), Quaternion.identity);
            var playerController = go.GetComponent<Player>();
            players.Add(playerController);
            playerController.playerMovement.listNodesTransform = map.GetListNodesTransform();
            playerController.playerMovement.dice = dice;
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
        Debug.Log(switchTurnButton.transform.position.x);
        TurnTransfered += RollEvents;
        RoundComplete += EventController.NotifyAllTurnsPassed;
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
        card.gameObject.SetActive(false);
        switchTurnButton.gameObject.SetActive(false);
        EventController.NotifyTurnPassed(_currentPlayer.playerClass);
        
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
        if(_indexPlayer<players.Count - 1) {
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
        DOYUIMover(switchTurnButton.gameObject, -Screen.height);

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

                    var currentPlayer = _currentPlayer.playerClass;
                    
                    businessController
                        .businessClass
                        .SetOwnerSameTypeBusinessesCountCallback(
                            () => GetBusinessesFor(currentPlayer).Count(business => business.Type == businessController.businessClass.Type)
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

    private void RollEvents()
    {
        var globalEvent = EventController.RollGlobalEvent();
        if (globalEvent is not null)
        {
            eventCard.Enqueue(globalEvent);
        }

        var localEvent = EventController.RollLocalEvent(_currentPlayer.playerClass);
        if (localEvent is null) 
            return;
        
        eventCard.Enqueue(localEvent);
        if (localEvent.Duration != 0) 
            return;
        
        ApplyLocalOneshotEvent(localEvent);
    }

    private void ApplyLocalOneshotEvent(Event localEvent)
    {
        var change = localEvent.GetDiceEffect();
        switch (localEvent.AffectedPlayerStat)
        {
            case AffectedPlayerStat.Fighters:
                if(change > 0)
                {
                    var fighterType = (FighterType)Random.Range(0, 3);
                    _currentPlayer.playerClass.AddFighters(fighterType, change);
                }
                else
                {
                    change = -change;
                    while (change > 0)
                    {
                        if (_currentPlayer.playerClass.CountFighter == 0)
                        {
                            break;
                        }
                        
                        var fighterType = (FighterType)Random.Range(0, 3);
                        var amountToRemove = _currentPlayer.playerClass.GetFighterCount(fighterType);
                        amountToRemove = Math.Min(amountToRemove, change);
                        _currentPlayer.playerClass.RemoveFighters(fighterType, amountToRemove);
                    } 
                }
                break;
            case AffectedPlayerStat.Money:
                var wallet = _currentPlayer.playerClass.Wallet;
                if (change > 0)
                {
                    wallet.AddMoney(change);
                }
                else
                {
                    wallet.TrySpendMoney(Math.Min(change, wallet.money));
                }
                break;
        }
    }
}
