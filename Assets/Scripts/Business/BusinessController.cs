using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BusinessController : MonoBehaviour
{
    [SerializeField] private string nameBusiness;    
    [SerializeField] private TypeBusiness type;
    [SerializeField] private SizeBusiness size;
    [SerializeField] private int price;
    //[SerializeField] private GameObject businessCard;

    [HideInInspector]
    public BusinessClass businessClass;

    private void Awake()
    {
        businessClass = new BusinessClass(price, nameBusiness, size, type);
    }

}
