using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UIAnimations;
public class BusinessCard : MonoBehaviour
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
        UpdateBusinessCard(business);
        //UpdateStrings(business);
        AnimateBusinessCard();
        buyButton.onClick.AddListener(buyCallback);
        buyButton.onClick.AddListener(() =>
        {
            buyButton.onClick.RemoveAllListeners();
        });
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
        typeIcon.sprite = typeSprites[(int)business.type];
        sizeIcon.sprite = sizeSprites[(int)business.size];
    }

    private void UpdateStrings(BusinessClass business)
    {
        sizeText.text = business.size.ToString();
        typeText.text = business.type.ToString();
        priceText.text = business.price.ToString();
        nameText.text = business.name.ToString();
    }
    
    private void AnimateBusinessCard()
    {
        YUIMover(gameObject, new Vector2(0, -Screen.height), -Screen.height / 8, 0.5f, LeanTweenType.easeOutExpo);
    }
}
