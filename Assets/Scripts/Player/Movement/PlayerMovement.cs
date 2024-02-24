using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(1)] private int speed;
    [SerializeField] private Dice dice;
    
    [HideInInspector]
    public bool isMoving = false;

    public List<Transform> listNodesTransform;
    [HideInInspector]
    public int currentIndex { get; private set; } = 0;
    
    private int _steps = 0;
    
    private void StartMoving()
    {
        dice.isRolled = false;
        _steps = dice.finalSide;
        isMoving = !isMoving;
    }
    
    public void MovePlayer(ref int steps)
    {
        _steps = steps;

        if (_steps != 0)
        {
            CheckMaxNode();
            MoveToNextNode();
            steps = _steps;
            if (_steps == 0)
            {
                isMoving = false;
                dice.isRolled = true;
            }
        }
    }
    
    private void MoveToNextNode()
    {
        Vector3 nextNode = listNodesTransform[currentIndex + 1].position;
        Vector3 newPosition = new Vector3(nextNode.x, transform.position.y, nextNode.z);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        if (CheckPosOnNode(nextNode)) {
            currentIndex++;
            _steps--;
        }
    }

    private bool CheckPosOnNode(Vector3 nextPos)
    {
        return transform.position.z == nextPos.z && transform.position.x == nextPos.x;
    }    
    
    private void CheckMaxNode()
    {
        if (currentIndex+1> listNodesTransform.Count-1) {
            currentIndex = -1;
        }
    }
}
