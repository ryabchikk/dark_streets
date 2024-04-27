using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotator : MonoBehaviour
{
    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        var rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward);
    }
}
