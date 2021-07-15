using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit Star");
        
    }
}
