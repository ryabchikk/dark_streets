using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField,Min(1)] private int speed;
    [SerializeField] private Wallet wallet;
    [SerializeField] private Dice dice;
    private List<Transform> _listNodesTransform;

    private int _steps = 0;
    private int _currentIndex = 0;
    
    private bool _isMoving=false;

    private void Awake()
    {
        _listNodesTransform = map.GetListNodesTransform();
    }

    private void FixedUpdate()
    {
        if(_isMoving) {
            Move();
        }
    }

    private void OnEnable()
    {
        dice.DiceRolled += StartMoving;
    }

    private void OnDisable()
    {
        dice.DiceRolled -= StartMoving;
    }
    
    private void StartMoving()
    {
        dice.isRolled = false;
        _steps = dice.finalSide;
        _isMoving = !_isMoving;
    }
    
    private void Move()
    {
        if (_steps != 0) {
            CheckMaxNode();
            MoveToNextNode();
        }
        else {
            _isMoving = false;
            dice.isRolled = true;
            wallet.TrySpendMoney(1000);
        }
    }


    
    private void MoveToNextNode()
    {
        Vector3 nextNode = _listNodesTransform[_currentIndex + 1].position;
        Vector3 newPosition = new Vector3(nextNode.x, transform.position.y, nextNode.z);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        if (CheckPosOnNode(nextNode)) {
            _currentIndex++;
            _steps--;
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
