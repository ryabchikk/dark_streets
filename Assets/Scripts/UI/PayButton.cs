using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PayButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private PlayerClass _currentPlayer;
    private BusinessClass _business;

    public void Init(PlayerClass currentPlayer, BusinessClass business)
    {
        _currentPlayer = currentPlayer;
        _business = business;
        text.text = $"ПЛОТИ {business.PriceForCellPass}";
        gameObject.SetActive(true); 
    }

    public void Click()
    {
        if (!_currentPlayer.Wallet.TrySpendMoney(_business.PriceForCellPass))
        {
            return;
        }
        
        _business.Owner.Wallet.AddMoney(_business.PriceForCellPass);
        gameObject.SetActive(false);
    }
}
