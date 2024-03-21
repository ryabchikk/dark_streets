using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings player")]
    [SerializeField] private int money;
    [SerializeField] private TypePlayer typePlayer;
    [SerializeField] private Wallet wallet;
    [SerializeField] private string name;
    
    [Header("Components player")]
    public PlayerMovement playerMovement;
    public Material playerNeutralMaterial;
    public Material playerLightMaterial;
    
    [HideInInspector]
    public PlayerClass playerClass;
    public string Name => name;

    public void Awake()
    {
        playerClass =  new PlayerClass(money, typePlayer, wallet);
    }
}
