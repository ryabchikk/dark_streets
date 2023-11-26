using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private void OnMouseDown()
    {
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
