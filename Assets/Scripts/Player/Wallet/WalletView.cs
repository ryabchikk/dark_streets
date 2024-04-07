using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UIAnimations;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountView;
    [SerializeField] private RectTransform startRect;
    private Wallet wallet;

    public void SetCurrentWallet(Wallet newWallet)
    {
        if(wallet is not null)
        {
            wallet.AmountChanged -= DisplayAmount;
        }
        
        wallet = newWallet;
        wallet.AmountChanged += DisplayAmount;
        DisplayAmount();
    }

    private void DisplayAmount()
    {
        int amount = wallet.money;
        amountView.text = amount.ToString();
        DOYUIMover(amountView.gameObject, startRect.anchoredPosition.y, 0.2f);
    }
}
