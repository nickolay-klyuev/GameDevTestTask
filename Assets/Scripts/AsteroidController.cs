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
        if (collision.gameObject.CompareTag("Laser"))
        {
            scoreDisplay.AddScore();
            collision.gameObject.SetActive(false);
            asteroidsMaker.SpawnAsteroid();
            Destroy(gameObject);
        }
    }
}
