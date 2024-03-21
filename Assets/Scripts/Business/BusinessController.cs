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
    [SerializeField] private GameObject businessLightColor;
    [SerializeField] private Material defaultNeutralMaterial;
    [SerializeField] private Material defaultLightMaterial;
    
    [HideInInspector]
    public BusinessClass businessClass;
    
    public Renderer BusinessNeutralColor { get; private set; }
    public Renderer BusinessLightColor { get; private set; }
    public Renderer[] BusinessNeutralColorChilds {  get; private set; }
    public Renderer[] BusinessLightColorChilds { get; private set; }
    
    public Material DefaultNeutralMaterial => defaultNeutralMaterial;
    public Material DefaultLightMaterial => defaultLightMaterial;
    
    private void Awake()
    {
        businessClass = new BusinessClass(price, nameBusiness, size, type);

        BusinessNeutralColor = businessNeutralColor.GetComponent<Renderer>();
        BusinessNeutralColorChilds = businessNeutralColor.GetComponentsInChildren<Renderer>();

        BusinessLightColor = businessLightColor.GetComponent<Renderer>();
        BusinessLightColorChilds = businessLightColor.GetComponentsInChildren<Renderer>();
    }
    
    public void ChangeBusinessColors(Material newLightMaterial, Material newNeutralMaterial)
    {
        ChangeColor(BusinessNeutralColor, newNeutralMaterial);
        ChangeColors(BusinessNeutralColorChilds, newNeutralMaterial);

        ChangeColor(BusinessLightColor, newLightMaterial);
        ChangeColors(BusinessLightColorChilds, newLightMaterial);
    }

    private void ChangeColor(Renderer rendererObject, Material newMaterial)
    {
        rendererObject.material = newMaterial;
    }

    private void ChangeColors(Renderer[] rendererObjects, Material newMaterial)
    {
        if (rendererObjects.Length != 0) { 
        
            foreach(Renderer rendererObject in rendererObjects) 
            {
                ChangeColor(rendererObject, newMaterial);
            }
        }
        else {
            return;
        }
    }
}
