using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        GameObject go = gameObject;
        go.SetActive(true);
        go.transform.localPosition = new Vector2(0, -Screen.height);
        gameObject.LeanMoveLocalY(-Screen.height/4, 0.5f).setEaseOutExpo();
    }
}
