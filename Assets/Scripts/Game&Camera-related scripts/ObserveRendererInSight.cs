using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class ObserveRendererInSight : MonoBehaviour
{
    [SerializeField] private Transform _objservedTransform;
    
    private Camera _cam;
    private bool isTriggered;
    
    public UnityEvent onBecameInvisible;

    private void Start()
    {
        isTriggered = false;
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 viewPos = _cam.WorldToViewportPoint(_objservedTransform.position);
        if ((viewPos.y > 1 || viewPos.y < 0)&&!isTriggered)
        {
            onBecameInvisible?.Invoke();
            isTriggered = true;
        }
    }
}
