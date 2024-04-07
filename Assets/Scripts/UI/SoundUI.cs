using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundUI : MonoBehaviour
{
    public void OnClickButton(Button button)
    {
        Debug.Log("Click");
        button.interactable = false;
    }
}
