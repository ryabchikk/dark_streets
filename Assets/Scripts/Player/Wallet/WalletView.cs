using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountView;
    [SerializeField] private Wallet wallet;

    private void OnDisable()
    {
        wallet.AmountChanged -= DisplayAmount;
    }

    private void OnEnable()
    {
        wallet.AmountChanged += DisplayAmount;
    }

    private void DisplayAmount()
    {
        int amount = wallet.money;
        amountView.text = amount.ToString();
    }
}
