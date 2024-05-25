using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEventsUI : MonoBehaviour
{
    [SerializeField] private EventController controller;
    [SerializeField] private GameObject minicard;
    [SerializeField] private EventCard eventCard;
    
    private List<EventMiniCard> _miniCardInstances = new();

    private void Start()
    {
        controller.GlobalEventsUpdated += Redraw;
    }

    private void OnDestroy()
    {
        controller.GlobalEventsUpdated -= Redraw;
    }

    private void Align(GameObject miniCard, int idx)
    {
        var rectTransform = miniCard.GetComponent<RectTransform>();
        var anchoredPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y - rectTransform.rect.height * idx);
    }
    
    private void Redraw()
    {
        var events = controller.GlobalEvents();
        for (int i = 0; i < events.Count; ++i)
        {
            if (i >= _miniCardInstances.Count)
            {
                var go = Instantiate(minicard, transform);
                _miniCardInstances.Add(go.GetComponent<EventMiniCard>());
                _miniCardInstances[i].Init(eventCard);
                Align(go, i);
            }

            var (ev, duration) = events[i];
            _miniCardInstances[i].UpdateEventMiniCard(ev, duration);
            _miniCardInstances[i].gameObject.SetActive(true);
        }

        for (int i = events.Count; i < _miniCardInstances.Count; ++i)
        {
            _miniCardInstances[i].gameObject.SetActive(false);
        }
    }
}
