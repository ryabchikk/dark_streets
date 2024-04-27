using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHelper : MonoBehaviour
{
    [SerializeField] private Sprite[] sizeSprites;
    [SerializeField] private Sprite[] typeSprites;
    [SerializeField] private AudioClip[] audioClips;
    private static ResourcesHelper _instance;
    
    private void Awake()
    {
        _instance = this;
    }

    public static Sprite GetTypeSprite(TypeBusiness type)
    {
        return _instance.typeSprites[(int)type];
    }

    public static Sprite GetSizeSprite(SizeBusiness size)
    {
        return _instance.sizeSprites[(int)size];
    }

    public static AudioClip GetTypeSound(TypeBusiness type)
    {
        return _instance.audioClips[(int)type];
    }
}
