using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        // Engelin üzerindeki fizik bileşenini yakala
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, -speed);
    }

    void Update()
    {
        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Rope"))
        {
            // Player etiketli böcekleri bul
            GameObject[] fireflies = GameObject.FindGameObjectsWithTag("Player");
            
            foreach (GameObject firefly in fireflies)
            {
                Rigidbody2D fireflyRb = firefly.GetComponent<Rigidbody2D>();
                if (fireflyRb != null)
                {
                    fireflyRb.constraints = RigidbodyConstraints2D.FreezeRotation; 
                }
            }
        }
    }
}