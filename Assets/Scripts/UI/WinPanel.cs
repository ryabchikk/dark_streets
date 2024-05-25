using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;

    public void Show(Player player)
    {
        playerNameText.text = player.Name;
        gameObject.SetActive(true);
    }
}
