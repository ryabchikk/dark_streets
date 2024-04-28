using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventMiniCard : MonoBehaviour
{
    [SerializeField] private Button openCardButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI durationText;
    
    private EventCard _eventCard;
    private Event _event;
    private int _duration;

    public void OpenEventCard()
    {
        if(!_eventCard.gameObject.activeSelf)
        {
            _eventCard.Enqueue(_event);
        }
    }

    public void Init(EventCard eventCard)
    {
        _eventCard = eventCard;
    }

    public void UpdateEventMiniCard(Event ev, int duration)
    {
        _event = ev;
        _duration = duration;

        nameText.text = _event.Name;
        durationText.text = _duration.ToString();
    }
}
