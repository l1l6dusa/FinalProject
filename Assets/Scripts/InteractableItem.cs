using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class InteractableItem : MonoBehaviour
{
    public UnityEvent CollisionWithPlayer;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNavigation>())
            
        {
            if (CollisionWithPlayer != null)
            {
                CollisionWithPlayer.Invoke();
            }
            else
            {
                Debug.Log("No listeners for this event");
            }   
        }
    }
}
