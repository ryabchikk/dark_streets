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
    [SerializeField] private GameObject businessNeutralColor;
    [SerializeField] private Material defaultMaterial;

    [HideInInspector]
    public BusinessClass businessClass;
    public Renderer BusinessNeutralColor { get; private set; }
    public Renderer[] BusinesNeutralColorChilds {  get; private set; }
    public Material DefaultMaterial => defaultMaterial;

    private void Awake()
    {
        businessClass = new BusinessClass(price, nameBusiness, size, type);

        BusinessNeutralColor = businessNeutralColor.GetComponent<Renderer>();
        BusinesNeutralColorChilds = businessNeutralColor.GetComponentsInChildren<Renderer>();
    }
}
