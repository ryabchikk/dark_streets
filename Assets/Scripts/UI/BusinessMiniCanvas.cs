using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BusinessMiniCanvas : MonoBehaviour
{
    [SerializeField] private BusinessController business;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI passiveIncomeText;
    [SerializeField] private TextMeshProUGUI priceForPassText;
    [SerializeField] private GameLoop gameLoop;
    [SerializeField] private PlayerBusinessCard businessCard;
    [SerializeField] private Image typeIcon;
    [SerializeField] private Image sizeIcon;

    private void Start()
    {
        business.businessClass.Updated += UpdateUI;
        gameLoop.AnyBusinessBought += UpdateUI;
        gameLoop.AnyBusinessSold += UpdateUI;
        gameLoop.EventsUpdated += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        business.businessClass.Updated -= UpdateUI;
        gameLoop.AnyBusinessBought -= UpdateUI;
        gameLoop.AnyBusinessSold -= UpdateUI;
        gameLoop.EventsUpdated -= UpdateUI;
    }

    public void OnClick()
    {
        if (gameLoop.PlayerModel == business.businessClass.Owner)
        {
            businessCard.ActivateBusinessCard(business.businessClass);
        }
    }

    private void UpdateStrings()
    {
        levelText.text = business.businessClass.Lvl.ToString();
        priceForPassText.text = business.businessClass.PriceForCellPass.ToString();
        passiveIncomeText.text = business.businessClass.PassiveIncome.ToString();
    }

    private void UpdateIcons()
    {
        typeIcon.sprite = ResourcesHelper.GetTypeSprite(business.businessClass.Type);
        sizeIcon.sprite = ResourcesHelper.GetSizeSprite(business.businessClass.Size);
    }

    private void UpdateUI()
    {
        UpdateStrings();
        UpdateIcons();
    }
}
