using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gameObject.transform.position, .1f);
    }
}
