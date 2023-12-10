using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField,Min(1)] private int speed;

    private List<Transform> _listNodesTransform;

    private int _steps;
    private int _currentIndex = 0;
    
    private bool _isMoving=false;

    private void Awake()
    {
        _listNodesTransform = map.GetListNodesTransform();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&!_isMoving) {
            _isMoving = true;
            _steps =Random.Range(1, 7);
            Debug.Log(_steps);
        }

        if(_isMoving) { 
            Move(ref _steps);
        }
    }

    private void Move(ref int steps)
    {
        if (steps != 0) {

            CheckMaxNode();
            MoveToNextNode(ref steps);
        }
        else {
            _isMoving = false;
        }
    }


    
    private void MoveToNextNode(ref int steps)
    {
        Vector3 nextNode = _listNodesTransform[_currentIndex + 1].position;
        Vector3 newPosition = new Vector3(nextNode.x, transform.position.y, nextNode.z);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        if (CheckPosOnNode(nextNode)) {
            _currentIndex++;
            steps--;
        }
    }

    private bool CheckPosOnNode(Vector3 nextPos)
    {
        return transform.position.z == nextPos.z && transform.position.x == nextPos.x;
    }    
    
    private void CheckMaxNode()
    {
        if (_currentIndex+1> _listNodesTransform.Count-1) {
            _currentIndex = -1;
        }
    }
}
