using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] diceSides;
    [SerializeField] private Image rend;
    [SerializeField,Range(2,6)] private int countSides;
    [HideInInspector]
    public bool isRolled;

    public int finalSide { get; private set; }
    public event UnityAction DiceRolled;

    private void Start()
    {
        isRolled = true;
    }

    public void RollClick()
    {
        if (isRolled) {
            RollTheDice();
        }
    }
    
    private void RollTheDice()
    {   
            finalSide = ChooseSideDice();
            rend.sprite = diceSides[finalSide-1];
            DiceRolled?.Invoke();
            Debug.Log("Final side: " + finalSide);
    }    
    
    private int ChooseSideDice()
    {
        return Random.Range(0, countSides) + 1;
    }
}
