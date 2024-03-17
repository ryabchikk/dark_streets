using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayButton : MonoBehaviour
{
    private PlayerClass _currentPlayer;
    private BusinessClass _business;

    public void Init(PlayerClass currentPlayer, BusinessClass business)
    {
        _currentPlayer = currentPlayer;
        _business = business;
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
