
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    [SerializeField] private LayerMask wallsLayer;
    
    private InputControls controls;
    private MeshRenderer[] arrowsMeshes;

    private void Start()
    {
        arrowsMeshes = GetComponentsInChildren<MeshRenderer>();
        wallsLayer = 1 << 6;
        HidePointer();
    }
    
    public void ShowPointer()
    {
        if (!EventSystem.current.IsPointerOverGameObject()){
            
            foreach (var renderer in arrowsMeshes)
            {
                renderer.enabled = !renderer.enabled;
            }
        }
}

    public void HidePointer()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            foreach (var renderer in arrowsMeshes)
            {
                renderer.enabled = !renderer.enabled;
            }
        }
    }
}
