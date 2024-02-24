using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UIAnimations;
public class BusinessCard : MonoBehaviour
{
    [SerializeField] private Text type;
    [SerializeField] private Text size;
    [SerializeField] private Text price;
    [SerializeField] private Text name;
    [SerializeField] private Button buyButton;
    
    public void ActivateBusinessCard(BusinessClass business, UnityAction buyCallback)
    {
        UpdateStrings(business);
        AnimateBusinessCard();
        buyButton.onClick.AddListener(buyCallback);
        buyButton.onClick.AddListener(() =>
        {
            buyButton.onClick.RemoveAllListeners();
        });
    }

    public void OnSuccessfulBuy()
    {
        // todo
        Debug.Log("Bought successfully");
    }

    public void OnUnsuccessfulBuy()
    {
        // todo
        Debug.Log("Could not buy");
    }

    private void UpdateStrings(BusinessClass business)
    {
        size.text = business.size.ToString();
        type.text = business.type.ToString();
        price.text = business.price.ToString();
        name.text = business.name.ToString();
    }
    
    private void AnimateBusinessCard()
    {
        YUIMover(gameObject, new Vector2(0, -Screen.height), -Screen.height / 8, 0.5f, LeanTweenType.easeOutExpo);
    }
}
