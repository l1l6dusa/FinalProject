using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Material _arrowMaterial;
    [SerializeField] private float _fadeInOutTime;
    
    private Color _arrowColor;
    private MeshRenderer[] _arrowsMeshes;
    private Coroutine _arrowVisibilityCoroutine;
    private WaitForEndOfFrame _waitForNextFrame;  
    
    private void Start()
    {
        _waitForNextFrame = new WaitForEndOfFrame();
        _arrowsMeshes = GetComponentsInChildren<MeshRenderer>();
        _arrowColor = _arrowMaterial.color;
        _arrowMaterial.color = new Color(_arrowColor.r, _arrowColor.g, _arrowColor.b, 0);
        HidePointer();
    }
    
    public void ShowPointer()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ActivateRenderer();
            if(_arrowVisibilityCoroutine != null)StopCoroutine(_arrowVisibilityCoroutine);
            _arrowVisibilityCoroutine = StartCoroutine(IncreaseAlphaCoroutine());
        }
    }

    public void HidePointer()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            DisableRenderer();
        }
    }

    private void DisableRenderer()
    {
        _arrowMaterial.color = new Color(_arrowColor.r, _arrowColor.g, _arrowColor.b, 0);
        foreach (var renderer in _arrowsMeshes)
        {
            renderer.enabled = false;
        }
    }
    
    private void ActivateRenderer()
    {
        foreach (var renderer in _arrowsMeshes)
        {
            renderer.enabled = true;
        }
    }
    
    private IEnumerator IncreaseAlphaCoroutine()
    {
        while (_arrowMaterial.color.a < 1f) 
        {
            var alpha = _arrowMaterial.color.a + (Time.deltaTime / _fadeInOutTime); 
            _arrowMaterial.color = new Color(_arrowColor.r, _arrowColor.g, _arrowColor.b, alpha);
            yield return _waitForNextFrame;
        }
    }
}
