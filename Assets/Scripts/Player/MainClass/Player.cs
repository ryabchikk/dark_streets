using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings player")]
    [SerializeField] private int money;
    [SerializeField] private TypePlayer typePlayer;
    
    [Header("Components player")]
    public PlayerMovement playerMovement;

    [HideInInspector]
    public PlayerClass playerClass;

    public void Awake()
    {
        playerClass =  new PlayerClass(money,typePlayer);
    }
}
