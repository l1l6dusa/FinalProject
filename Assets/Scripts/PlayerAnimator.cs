using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    [SerializeField] private string _dashAnimationName;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Animator _anim;
    
    // Start is called before the first frame update
    
    

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat(_dashAnimationName,Mathf.Clamp(_rb.velocity.magnitude, 0, 1));
    }
}
