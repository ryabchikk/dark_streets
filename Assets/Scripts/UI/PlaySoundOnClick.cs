using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject dummy;

    public void OnClick()
    {
        var go = Instantiate(dummy);
        go.GetComponent<DummySoundButton>().Init(audioSource);
    }
}
