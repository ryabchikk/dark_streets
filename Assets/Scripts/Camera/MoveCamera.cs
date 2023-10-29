using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int speed;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVector = Vector3.zero;

        moveVector.x = Input.GetAxis("Horizontal") * (speed *Time.deltaTime);
        moveVector.z = Input.GetAxis("Vertical") * (speed * Time.deltaTime);
        
        transform.position += moveVector;
    }
}
