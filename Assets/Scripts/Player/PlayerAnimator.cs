
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    
    [SerializeField] private string _dashAnimationName;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    
    private void Update()
    {
        _animator.SetFloat(_dashAnimationName,Mathf.Clamp(_rigidbody.velocity.magnitude, 0, 1));
    }
}
