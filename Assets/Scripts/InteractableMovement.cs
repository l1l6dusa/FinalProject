using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class InteractableMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject[] _anchors;
    
    private int _index;
    private bool _isAsc;
    private Rigidbody _rb;
    private Vector3 _direction;
    private float _distanceThreshold;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }
    
    private void Start()
    {
        _distanceThreshold = 0.25f;
        _index = 0;
        _direction = (_anchors[_index].transform.position - transform.position).normalized;
    }
    
    private void FixedUpdate()
    {
        MoveToAnchor();
        TrySetNextAnchor();
    }

    private void TrySetNextAnchor()
    {
        if (Vector3.Distance(transform.position, _anchors[_index].transform.position)<_distanceThreshold)
        {
            if(_index == 0)
            {
                _isAsc = true;
            }
            if(_index == _anchors.Length-1)
            {
                _isAsc = false;
            }
            if (_isAsc)
            {
                _index++;
            }
            else
            {
                _index--;
            }
            _direction = (_anchors[_index].transform.position - transform.position).normalized;
        }
    }

    private void MoveToAnchor()
    { 
        _rb.MovePosition((_speed * Time.fixedDeltaTime * _direction) + transform.position);
    }
}
