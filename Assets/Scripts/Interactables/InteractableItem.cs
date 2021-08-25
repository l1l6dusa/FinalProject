using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class InteractableItem : MonoBehaviour
{
    public UnityEvent CollidedWithPlayer;
    
    [SerializeField] private bool _destroyItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNavigation>())
        {
            if (CollidedWithPlayer != null)
            {
                CollidedWithPlayer.Invoke();
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
