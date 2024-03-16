using System;
using System.Collections;
using System.Collections.Generic;
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

    [Space]
    [Header("Icons Sprites")]
    [SerializeField] private Sprite[] typeSprites;
    [SerializeField] private Sprite[] sizeSprites;

    private BusinessClass _currentBusiness;
    private PlayerBusinessCard _playerBusinessCard;

    public void Init(GameObject playerBusinessCard)
    {
        _playerBusinessCard = playerBusinessCard.GetComponent<PlayerBusinessCard>();
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
        typeIcon.sprite = typeSprites[(int)business.Type];
        sizeIcon.sprite = sizeSprites[(int)business.Size];
    }

    private void UpdateStrings()
    {
        levelText.text = _currentBusiness.Lvl.ToString();
    }
}
