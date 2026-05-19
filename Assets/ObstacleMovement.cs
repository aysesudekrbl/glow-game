using UnityEngine;


public class ObstacleMovement : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

}
