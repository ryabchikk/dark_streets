using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BusinessMiniCard : MonoBehaviour
{
    [Space]
    [Header("Business Icons")]
    [SerializeField] private Image typeIcon;
    [SerializeField] private Image sizeIcon;

    [Space]
    [Header("Icons Sprites")]
    [SerializeField] private Sprite[] typeSprites;
    [SerializeField] private Sprite[] sizeSprites;

    private void UpdateBusinessMiniCard(BusinessClass business)
    {
        UpdateIcons(business);
    }

    private void UpdateIcons(BusinessClass business)
    {
        typeIcon.sprite = typeSprites[(int)business.type];
        sizeIcon.sprite = sizeSprites[(int)business.size];
    }

}
