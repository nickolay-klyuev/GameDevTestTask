using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private AsteroidsMaker asteroidsMaker;
    private GhostObjectsMaker ghosts;
    private ScoreDisplay scoreDisplay;

    void Start()
    {
        asteroidsMaker = GameObject.Find("Asteroids Maker").GetComponent<AsteroidsMaker>();
        ghosts = GetComponent<GhostObjectsMaker>();
        scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<ScoreDisplay>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        bool isLaser = collision.gameObject.CompareTag("Laser");
        bool isPlayer = collision.gameObject.CompareTag("Player");
        
        if (isLaser || isPlayer)
        {
            if (isLaser)
            {
                collision.gameObject.SetActive(false);
                scoreDisplay.AddScore();
            }

            asteroidsMaker.SpawnAsteroid();
            Destroy(gameObject);
        }
    }
}
