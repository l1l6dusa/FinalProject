using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class InteractableItem : MonoBehaviour
{
    [SerializeField] private bool _destroyItem;
    
    public UnityEvent CollisionWithPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNavigation>())
        {
            if (CollisionWithPlayer != null)
            {
                CollisionWithPlayer.Invoke();
                if (_destroyItem)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Debug.Log("No listeners for this event");
            }   
        }
    }
}
