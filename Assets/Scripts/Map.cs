using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> listNodes = new List<GameObject>();
    
    private List<Transform> listNodesTransform = new List<Transform>();

    private void Awake()
    {
        foreach (GameObject node in listNodes) {
            listNodesTransform.Add(node.transform);
        }
    }

    public BusinessController GetBusinessAt(int index)
    {
        return listNodes[index].GetComponent<BusinessController>();
    }

    public List<Transform> GetListNodesTransform() { return listNodesTransform; }
    public List<GameObject> GetListNodes() { return listNodes; }
}
