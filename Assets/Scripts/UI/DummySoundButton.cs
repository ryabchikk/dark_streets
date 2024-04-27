using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummySoundButton : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    public void Init(AudioSource source)
    {
        audioSource.clip = source.clip;
        audioSource.Play();
        StartCoroutine(DestroyOnAudioEnd());
    }

    private IEnumerator DestroyOnAudioEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
