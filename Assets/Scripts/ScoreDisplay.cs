using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void AddScore(int amount = 100)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }
}
