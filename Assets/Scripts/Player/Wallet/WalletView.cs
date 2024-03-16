using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountView;
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
    }
}
