
using UnityEngine;


public class CollectibleCollision : MonoBehaviour
{
    
void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Firefly"))
        {
            Destroy(gameObject);
        }
    }
}
