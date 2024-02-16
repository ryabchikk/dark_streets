using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BusinessController : MonoBehaviour
{
    [SerializeField] private GameObject businessCard;
    [SerializeField] private int price;
    [SerializeField] private TypeBusiness typeBusiness;
    [SerializeField] private SizeBusiness sizeBusiness;

    public BusinessClass businessClass;

    private void Awake()
    {
        businessClass = new BusinessClass(price, sizeBusiness, typeBusiness);
    }

}
