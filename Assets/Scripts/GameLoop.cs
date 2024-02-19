using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private Player[] players;
    [SerializeField] private Map map;
    [SerializeField] private Dice dice;
    [SerializeField] private BusinessCard card;
    private Player currentPlayer;
    private int _indexPlayer = 0;
    private int _steps = 0;

    private void Awake()
    {
        foreach (Player player in players) 
        { 
            player.playerMovement.listNodesTransform = map.GetListNodesTransform();
        }

        currentPlayer = players[_indexPlayer];
        
    }

    private void FixedUpdate()
    {
        if (currentPlayer.playerMovement.isMoving) { 
            currentPlayer.playerMovement.MovePlayer(ref _steps);
            CheckZeroSteps();
        }
        else {
            dice.isRolled = true;
        }
        
    }
    
    private void OnEnable()
    {
        dice.DiceRolled += StartMovingPlayer;
    }

    private void OnDisable()
    {
        dice.DiceRolled -= StartMovingPlayer;
    }

    private void StartMovingPlayer()
    {
        dice.isRolled = false;
        _steps = dice.finalSide;
        currentPlayer.playerMovement.isMoving = true;
    }

    private void UpdateCurrentPlayer()
    {
        if(_indexPlayer<players.Length - 1) {
            _indexPlayer++; 
        }
        else {
            _indexPlayer = 0;
        }

        currentPlayer = players[_indexPlayer];
    }

    private void CheckZeroSteps()
    {
        if (_steps == 0) {
            
            if (map.GetListNodes()[currentPlayer.playerMovement.currentIndex].GetComponent<BusinessController>()) {

                BusinessController businessController = map.GetListNodes()[currentPlayer.playerMovement.currentIndex].GetComponent<BusinessController>();
                
                currentPlayer.playerClass.AddNewBusinessIndex(currentPlayer.playerMovement.currentIndex);
                card.ActivateBusinessCard(businessController.businessClass);
                
                Debug.Log("Count business: " + currentPlayer.playerClass.businessIndex.Count);
            }

            UpdateCurrentPlayer();
        }
    }
}
