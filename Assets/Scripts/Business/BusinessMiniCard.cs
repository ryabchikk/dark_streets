using System;
using System.Collections;
using System.Collections.Generic;
using Business;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BusinessMiniCard : MonoBehaviour
{
    [Space] 
    [Header("UI elements")] 
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Space]
    [Header("Business Icons")]
    [SerializeField] private Image typeIcon;
    [SerializeField] private Image sizeIcon;

    private BusinessClass _currentBusiness;
    private PlayerBusinessCard _playerBusinessCard;
    private GameObject _defenceCard;

    public void Init(GameObject playerBusinessCard, GameObject defenceCard)
    {
        _playerBusinessCard = playerBusinessCard.GetComponent<PlayerBusinessCard>();
        _defenceCard = defenceCard;
    }
    
    public void UpdateBusinessMiniCard(BusinessClass business)
    {
        if (_currentBusiness is not null)
        {
            _currentBusiness.LevelChanged -= UpdateStrings;
        }
        _currentBusiness = business;
        _currentBusiness.LevelChanged += UpdateStrings;
        UpdateIcons(_currentBusiness);
        UpdateStrings();
    }

    private void OnDisable()
    {
        _currentBusiness.LevelChanged -= UpdateStrings;
        _currentBusiness = null;
    }

    public void FindBusiness()
    {
        _playerBusinessCard.ActivateBusinessCard(_currentBusiness);
        _playerBusinessCard.gameObject.SetActive(true);
    }

    public void OpenDefenceCard()
    {
        _defenceCard.GetComponent<DefenceCard>().ActivateBusinessCard(_currentBusiness);
        _defenceCard.SetActive(true);
    }

    public void Upgrade()
    {
        _currentBusiness.TryUpgrade();
    }

    public void Downgrade()
    {
        _currentBusiness.TryDowngrade();
    }

    private void UpdateIcons(BusinessClass business)
    {
        typeIcon.sprite = ResourcesHelper.GetTypeSprite(business.Type);
        sizeIcon.sprite = ResourcesHelper.GetSizeSprite(business.Size);
    }

    private void UpdateStrings()
    {
        levelText.text = _currentBusiness.Lvl.ToString();
    }
}
