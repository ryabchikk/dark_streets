using System;
using System.Collections;
using System.Collections.Generic;
using Business;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UIAnimations;

public class PlayerBusinessCard : BusinessCardBase
{
    [Header("Business Strings")]
    [SerializeField] private Text typeText;
    [SerializeField] private Text sizeText;
    [SerializeField] private Text priceText;
    [SerializeField] private Text nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Space]
    [Header ("Business Icons")]
    [SerializeField] private Image typeIcon;
    [SerializeField] private Image sizeIcon;
    
    [Space] 
    [Header("Objects")] 
    [SerializeField] private GameObject defenceCard;
    
    [Space]
    [Header ("Icons Sprites")]
    [SerializeField] private Sprite[] typeSprites;
    [SerializeField] private Sprite[] sizeSprites;

    protected override void ActivateBusinessCardImpl()
    {
        UpdateBusinessCard();
        AnimateBusinessCard();
        _currentBusiness.LevelChanged += UpdateBusinessCard;
    }

    private void OnDisable()
    {
        _currentBusiness.LevelChanged -= UpdateBusinessCard;
    }

    public void OpenDefenceCard()
    {
        defenceCard.GetComponent<DefenceCard>().ActivateBusinessCard(_currentBusiness);
        defenceCard.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Sell()
    {
        _currentBusiness.Sell();
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        _currentBusiness.TryUpgrade();
    }

    public void Downgrade()
    {
        _currentBusiness.TryDowngrade();
    }

    private void UpdateBusinessCard()
    {
        UpdateStrings(_currentBusiness);
        UpdateIcons(_currentBusiness);
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
        priceText.text = business.SellPrice.ToString();
        nameText.text = business.Name;
        levelText.text = business.Lvl.ToString();
    }
    
    private void AnimateBusinessCard()
    {
        YUIMover(gameObject, -Screen.height);
    }
}
