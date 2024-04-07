using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummySoundButton : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private AudioSource audioSource;
    private RectTransform _parentRectTransform;
    private Button _parentButton;
    
    private void Start()
    {
        _parentRectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
        _parentButton = transform.parent.gameObject.GetComponent<Button>();

        rectTransform.anchoredPosition = new Vector2(0, 0);
        var sizeDelta = _parentRectTransform.sizeDelta;
        Debug.Log(sizeDelta);
        rectTransform.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
    }

    public void OnClick()
    {
        audioSource.Play();
        Debug.Log("Click");
        if (_parentButton.enabled)
        {
            _parentButton.onClick?.Invoke();
        }
    }
}
