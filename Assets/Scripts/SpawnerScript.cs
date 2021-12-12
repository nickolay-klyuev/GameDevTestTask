using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;

    private float offset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnAsteroid();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAsteroid()
    {
        Vector3 initPos = new Vector3(Random.Range(offset, GhostObjectsMaker.screenWidth / 2) * (Random.Range(0, 2) == 0 ? -1 : 1), 
                                    Random.Range(offset, GhostObjectsMaker.screenHeight / 2) * (Random.Range(0, 2) == 0 ? -1 : 1), 0);

        Rigidbody2D asteroidRb = Instantiate(asteroid, initPos, Quaternion.identity).GetComponent<Rigidbody2D>();
        asteroidRb.AddTorque(Random.Range(-25f, 25f));
        asteroidRb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(50f, 200f));
    }
}
