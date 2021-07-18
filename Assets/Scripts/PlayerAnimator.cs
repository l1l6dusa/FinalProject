
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    [SerializeField] private string _dashAnimationName;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    
    void Update()
    {
        _anim.SetFloat(_dashAnimationName,Mathf.Clamp(_rb.velocity.magnitude, 0, 1));
    }
}
