using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllBusinessesUI : MonoBehaviour
{
    [SerializeField] private GameLoop gameLoop;
    [SerializeField] private GameObject miniCardPrefab;
    [SerializeField] private GameObject playerBusinessCard;
    [SerializeField] private GameObject defenceCard;

    private List<BusinessMiniCard> _miniCardInstances = new();

    private void Awake()
    {
        gameLoop.TurnTransfered += RedrawBusinesses;
        gameLoop.AnyBusinessBought += RedrawBusinesses;
        gameLoop.AnyBusinessSold += RedrawBusinesses;
    }

    private void OnDestroy()
    {
        gameLoop.TurnTransfered -= RedrawBusinesses;
        gameLoop.AnyBusinessBought -= RedrawBusinesses;
        gameLoop.AnyBusinessSold -= RedrawBusinesses;
    }

    private void OnEnable()
    {
        RedrawBusinesses();
    }

    private void Align(GameObject miniCard, int idx)
    {
        var rectTransform = miniCard.GetComponent<RectTransform>();
        var anchoredPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y - rectTransform.rect.height * idx);
    }

    private void RedrawBusinesses()
    {
        var businesses = gameLoop.GetBusinessesForCurrentPlayer().ToList();
        for (int i = 0; i < businesses.Count; ++i)
        {
            if (i >= _miniCardInstances.Count)
            {
                var go = Instantiate(miniCardPrefab, transform);
                _miniCardInstances.Add(go.GetComponent<BusinessMiniCard>());
                _miniCardInstances[i].Init(playerBusinessCard, defenceCard);
                Align(go, i);
            }
            
            _miniCardInstances[i].UpdateBusinessMiniCard(businesses[i]);
            _miniCardInstances[i].gameObject.SetActive(true);
        }

        for (int i = businesses.Count; i < _miniCardInstances.Count; ++i)
        {
            _miniCardInstances[i].gameObject.SetActive(false);
        }
    }
}
