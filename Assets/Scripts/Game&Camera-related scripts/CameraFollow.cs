using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject followedObject;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxZdiff;
    [SerializeField] private float _maxXdiff;
    [SerializeField] private float _cameraConstantSpeed;
    [SerializeField] private float _timeOffset;
    private float _zOffset;
    private Coroutine _xcoroutine;
    private Coroutine _cameraBackMovement;
    
    private float xDiff;
    private float zDiff;
    

    void Start()
    {
        _zOffset =  followedObject.transform.position.z - transform.position.z;
    }
    
    void Update() 
    {
        if(_cameraBackMovement==null)
            _cameraBackMovement = StartCoroutine(CameraBackMovement());
        zDiff = followedObject.transform.position.z - transform.position.z - _zOffset;
        xDiff = Mathf.Abs(followedObject.transform.position.x-transform.position.x);
        if (zDiff > _maxZdiff)
        {
            /*if(_cameraBackMovement!=null)
                StopCoroutine(_cameraBackMovement);*/
                //_cameraBackMovement = null;
            var translationVector = new Vector3(0, 0, followedObject.transform.position.z + _zOffset) * (_speed * Time.deltaTime);
            transform.position +=  translationVector;
        }
        else
        {
            /*if(_cameraBackMovement==null)
                _cameraBackMovement = StartCoroutine(CameraBackMovement());*/
        }
        
        if (xDiff > _maxXdiff)
        {
            if (_xcoroutine != null)
            {
                StopCoroutine(_xcoroutine);
                _xcoroutine = StartCoroutine(MoveToFollowedObject(followedObject.transform.position.x));
            }
            else
            {
                _xcoroutine = StartCoroutine(MoveToFollowedObject(followedObject.transform.position.x)); 
            }
                
        }
    }

    IEnumerator MoveToFollowedObject(float targetXPosition)
    {
        if (xDiff > 0)
        {
            transform.position +=  new Vector3(targetXPosition - transform.position.x, 0, 0) * (_speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator CameraBackMovement()
    {
        yield return new WaitForSeconds(_timeOffset);
        while (true)
        {
            transform.position += Time.deltaTime * _cameraConstantSpeed * Vector3.forward;
            yield return null;
        }
    }
}
