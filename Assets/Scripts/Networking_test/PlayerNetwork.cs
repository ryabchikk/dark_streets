using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
  private  void Update()
  {
    Vector3 move = new Vector3(0,0,0);
    if(Input.GetKey(KeyCode.W)) move.z = 1f;
    if(Input.GetKey(KeyCode.S)) move.z = -1f;
    if(Input.GetKey(KeyCode.A)) move.x = -1f;
    if(Input.GetKey(KeyCode.D)) move.x = 1f;

    float moveSpeed = 3f;
    transform.position += move * moveSpeed*Time.deltaTime;
  }
}
