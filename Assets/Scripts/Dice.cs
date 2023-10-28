using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dice : MonoBehaviour
{
    public string diceSidesFolder = "DiceSides";
    public SpriteRenderer rend;
    public float animationDuration = 1.5f;

    private bool isRolling = false;
    private int finalSide = -1;

    private Sprite[] diceSides; 
    private float rollTimer = 0.0f;


    private void Awake()
    {
        rend = GetComponent <SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>(diceSidesFolder);
    }
    private void OnMouseDown()
    {
        if (!isRolling)
        {
            StartCoroutine(RollTheDice());
        }
    }

    private IEnumerator RollTheDice()
    {
        isRolling = true;
        finalSide = -1;
        rollTimer = 0.0f;

        int randomDiceSide = 0; 

        while (rollTimer < animationDuration)
        {
            float t = rollTimer / animationDuration;
            randomDiceSide = Random.Range(0, diceSides.Length);
            rend.sprite = diceSides[randomDiceSide];

            yield return null;

            rollTimer += Time.deltaTime;
        }

        finalSide = randomDiceSide + 1;
        Debug.Log("Final side: " + finalSide);

        isRolling = false;
    }
}
