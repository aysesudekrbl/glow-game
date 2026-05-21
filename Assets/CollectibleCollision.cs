
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class CollectibleCollision : MonoBehaviour
{
    public AudioClip collectSound;
void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Firefly"))
        {
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            ScoreController.instance.addScore();
            Destroy(gameObject);
        }
    }
}
