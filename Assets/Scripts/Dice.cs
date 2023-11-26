using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] diceSides;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField,Range(2,6)] private int countSides;

    private void OnMouseDown()
    {
        RollTheDice();
    }

    private void RollTheDice()
    {
        int finalSide = ChooseSideDice();
        rend.sprite = diceSides[finalSide-1];
        Debug.Log("Final side: " + finalSide);
    }    
    
    private int ChooseSideDice()
    {
        return Random.Range(0, countSides) + 1;
    }
}
