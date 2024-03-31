using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCreation : MonoBehaviour
{
    [SerializeField] private int maxPlayersCount;
    [SerializeField] private int minPlayersCount;
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private TextMeshProUGUI countText;
    public static int PlayersCount = 2;

    private void Start()
    {
        PlayersCount = minPlayersCount;
        countText.text = PlayersCount.ToString();
    }

    public void IncreaseCount()
    {
        if (PlayersCount < maxPlayersCount)
        {
            ++PlayersCount;
        }

        if (PlayersCount >= maxPlayersCount)
        {
            increaseButton.interactable = false;
        }

        decreaseButton.interactable = true;

        countText.text = PlayersCount.ToString();
    }
    
    public void DecreaseCount()
    {
        if (PlayersCount > minPlayersCount)
        {
            --PlayersCount;
        }

        if (PlayersCount <= minPlayersCount)
        {
            decreaseButton.interactable = false;
        }

        increaseButton.interactable = true;
        
        countText.text = PlayersCount.ToString();
    }
}
