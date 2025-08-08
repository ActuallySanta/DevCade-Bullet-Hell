using UnityEngine;
using TMPro;

public class DisplayScoreOnGameOver : MonoBehaviour
{
    private TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();

        if (GamePlayManager.Instance != null) scoreText.text = $"SCORE: " + GamePlayManager.Instance.currentScore;
    }
}
