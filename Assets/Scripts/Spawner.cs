using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;
    public GameObject flyingDinoPrefab;

    public static List<GameObject> obstacles = new List<GameObject>();

    public static float timer;
    public float maxTime;

    private void Start()
    {
        timer = 1.51f;
    }

    void Update()
    {
        if (timer > maxTime)
        {
            if (Random.Range(0,1f) < 0.8)
            {
                int rnd = Random.Range(0, ObstaclePrefabs.Length);

                GameObject obstacle = Instantiate(ObstaclePrefabs[rnd]);
                obstacle.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                obstacles.Add(obstacle);

                timer = 0;
                maxTime = Random.Range(0.8f, 1f);
            }

            else
            {
                GameObject flyingDino = Instantiate(flyingDinoPrefab);
                flyingDino.transform.position = new Vector3(transform.position.x, -1.1f, 1);
                obstacles.Add(flyingDino);

                timer = 0;
                maxTime = Random.Range(1f, 1.2f);
            }
        }

        timer += Time.deltaTime;
    }
}
