using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UIAnimations;
public class BusinessCard : BusinessCardBase
{
    [Header("Business Strings")]
    [SerializeField] private Text typeText;
    [SerializeField] private Text sizeText;
    [SerializeField] private Text priceText;
    [SerializeField] private Text nameText;
    [SerializeField] private Button buyButton;

    [Space]
    [Header ("Business Icons")]
    [SerializeField] private Image typeIcon;
    [SerializeField] private Image sizeIcon;
    
    [Space]
    [Header ("Icons Sprites")]
    [SerializeField] private Sprite[] typeSprites;
    [SerializeField] private Sprite[] sizeSprites;

    public void ActivateBusinessCard(BusinessClass business, UnityAction buyCallback)
    {
        ActivateBusinessCard(business);
        buyButton.onClick.AddListener(buyCallback);
        buyButton.onClick.AddListener(() =>
        {
            buyButton.onClick.RemoveAllListeners();
        });
    }

    protected override void ActivateBusinessCardImpl()
    {
        UpdateBusinessCard(_currentBusiness);
        AnimateBusinessCard();
    }

    public void OnSuccessfulBuy()
    {
        Debug.Log("Bought successfully");
        gameObject.SetActive(false);
    }

    public void OnUnsuccessfulBuy()
    {
        // todo
        Debug.Log("Could not buy");
    }

    private void UpdateBusinessCard(BusinessClass business)
    {
        UpdateStrings(business);
        UpdateIcons(business);
    }

    private void UpdateIcons(BusinessClass business)
    {
        typeIcon.sprite = typeSprites[(int)business.Type];
        sizeIcon.sprite = sizeSprites[(int)business.Size];
    }

    private void UpdateStrings(BusinessClass business)
    {
        sizeText.text = business.Size.ToString();
        typeText.text = business.Type.ToString();
        priceText.text = business.BuyPrice.ToString();
        nameText.text = business.Name.ToString();
    }
    
    private void AnimateBusinessCard()
    {
        YUIMover(gameObject, -Screen.height);
    }
}
