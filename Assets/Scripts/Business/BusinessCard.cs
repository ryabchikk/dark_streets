using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCard : MonoBehaviour
{
    [SerializeField] private Text type;
    [SerializeField] private Text size;
    [SerializeField] private Text price;

    public void ActivateBusinessCard(BusinessClass business)
    {
        UpdateStrings(business);
        AnimateBusinessCard();
    }

    private void UpdateStrings(BusinessClass business)
    {
        size.text = business.sizeBusiness.ToString();
        type.text = business.typeBusiness.ToString();
        price.text = business.price.ToString();
    }
    private void AnimateBusinessCard()
    {
        gameObject.SetActive(true);
        gameObject.transform.localPosition = new Vector2(0, -Screen.height);
        gameObject.LeanMoveLocalY(-Screen.height/4, 0.5f).setEaseOutExpo();
    }
}
