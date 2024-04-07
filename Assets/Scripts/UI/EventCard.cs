using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UIAnimations;
public class EventCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI effectText;

    private Queue<Event> _queue = new();

    public void Enqueue(Event ev)
    {
        _queue.Enqueue(ev);
        if (!gameObject.activeSelf)
        {
            Activate();
        }
    }
    
    private void Activate()
    {
        var ev = _queue.Dequeue();
        nameText.text = ev.Name;
        descriptionText.text = ev.Description;
        effectText.text = ev.EffectDescription;
        DOYUIMover(gameObject, -Screen.height,0.5f,Ease.OutBack);
        gameObject.SetActive(true);
    }

    public void OnClick()
    {
        if(_queue.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        Activate();
    }
}
