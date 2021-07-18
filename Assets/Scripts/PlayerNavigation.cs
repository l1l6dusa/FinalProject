using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RbMovement))]
public class PlayerNavigation : MonoBehaviour
{
    [SerializeField] private LayerMask _wallsLayer;
    [SerializeField] private float _shootDistance;
    [SerializeField] private float _thresholdAngle;
    [SerializeField] private float _deadZone;
    [SerializeField] private Pointer _pointer;
    [SerializeField] private float _pointerRotationSpeed;

    private Transform _pointerTransform;
    private float _xPointer;
    private float _zPointer;
    private Coroutine _arrowRoutine;
    private Quaternion _rotationAnchor;
    private RbMovement _playerMovement;
    private InputControls _controls;
    private Vector2 _position;
    private float _offsetMultiplier = 0.1f;

    

    private void Awake()
    {
        _controls = new InputControls();
        _controls.Main.ShowPointer.started += ctx => _pointer.ShowPointer();
        _controls.Main.Shoot.performed += ctx => ShootPlayer();
        _controls.Main.Shoot.performed += ctx => _pointer.HidePointer();
        _controls.Main.ShowPointer.started += ctx => RotateArrow();
        _controls.Main.Shoot.performed += ctx => CancelArrowRotate();
    }

    private void Start() {
        _pointerTransform = _pointer.transform;
        _xPointer = _pointerTransform.localPosition.x;
        _zPointer = _pointerTransform.localPosition.z;
        _playerMovement = GetComponent<RbMovement>();
        _rotationAnchor = Quaternion.LookRotation(transform.forward);
    }
    
    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void ShootPlayer()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            var ray = new Ray(transform.position + new Vector3(0, 0.05f, 0), _pointer.transform.forward);
            if (Physics.Raycast(ray, out var hitInfo, _shootDistance, _wallsLayer))
            {
                var direction = transform.position - hitInfo.point;
                var offset = hitInfo.normal * _offsetMultiplier;
                
                if (direction.magnitude > _deadZone) 
                {
                    
                    _rotationAnchor = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
                    _playerMovement.SetPositionAndRotation(hitInfo.point + offset, _rotationAnchor, hitInfo.normal);
                }
                var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = hitInfo.point + offset;
                sphere.GetComponent<Collider>().enabled = false;
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }

    private void RotateArrow()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _arrowRoutine = StartCoroutine(RotateRoutine(_thresholdAngle, _rotationAnchor));
        }
    }

    private void CancelArrowRotate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            StopCoroutine(_arrowRoutine);
        }
    }

    public IEnumerator RotateRoutine(float bonds, Quaternion anchorRotation)
    {
        var yrotation = anchorRotation.eulerAngles.y + 360;
        var minBond = yrotation - bonds;
        var maxBond = yrotation + bonds;
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            var pingPongValue = Mathf.PingPong(Time.time * _pointerRotationSpeed, 1);
            var lerpedValue = Mathf.Lerp(minBond, maxBond, pingPongValue);
            var xval = _zPointer * Mathf.Sin(lerpedValue * Mathf.Deg2Rad);
            var zval = _zPointer * Mathf.Cos(lerpedValue * Mathf.Deg2Rad);
            _pointerTransform.rotation = Quaternion.Euler(0, lerpedValue, 0);
            _pointerTransform.localPosition = new Vector3(xval, _pointer.transform.localPosition.y, zval);
            yield return fixedUpdate;
        }
    }
}
