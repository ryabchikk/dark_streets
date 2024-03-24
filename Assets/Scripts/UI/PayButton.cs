using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PayButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private PlayerClass _currentPlayer;
    private BusinessClass _business;
    private Action OnPayCallback;

    public void Init(PlayerClass currentPlayer, BusinessClass business, Action callback)
    {
        _currentPlayer = currentPlayer;
        _business = business;
        text.text = $"ПЛОТИ {business.PriceForCellPass}";
        OnPayCallback = callback;
        gameObject.SetActive(true); 
    }

    public void Click()
    {
        if (!_currentPlayer.Wallet.TrySpendMoney(_business.PriceForCellPass))
        {
            return;
        }
        
        _business.Owner.Wallet.AddMoney(_business.PriceForCellPass);
        OnPayCallback?.Invoke();
        gameObject.SetActive(false);
    }
}
