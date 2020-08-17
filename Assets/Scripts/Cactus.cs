using UnityEngine;

public class Cactus : MonoBehaviour
{
    public static float speed = 3;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -4.5f)
        { 
            Spawner.obstacles.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
