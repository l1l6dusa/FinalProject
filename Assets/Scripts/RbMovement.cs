using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class RbMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToTarget;
    [SerializeField] private GameObject _player;
    private Rigidbody _rigidbody;
    private Vector3 _point;
    private Vector3 _forwardRotation;
    public UnityEvent OnMovementStart;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _point = transform.position;
    }

    private void FixedUpdate()
    {
        
        if(Vector3.Distance(transform.position, _point) > _distanceToTarget)
        {
            var tempPos = (_point - transform.position) * (Time.deltaTime * _speed);
            _rigidbody.MovePosition(transform.position+tempPos);
        }
        else {
            _player.transform.rotation = Quaternion.LookRotation(_forwardRotation, Vector3.up);
        }
    }

    public void SetPositionAndRotation(Vector3 point, Quaternion rotation, Vector3 forwardRotation) {
        _point = point;
        _forwardRotation = forwardRotation;
        OnMovementStart?.Invoke();
    }
}
