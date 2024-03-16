using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BusinessCardBase : MonoBehaviour
{
    protected BusinessClass _currentBusiness;

    public void ActivateBusinessCard(BusinessClass business)
    {
        _currentBusiness = business;
        ActivateBusinessCardImpl();
    }

    protected abstract void ActivateBusinessCardImpl();
}
