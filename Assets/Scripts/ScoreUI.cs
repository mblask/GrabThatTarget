using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnUpdateScore += updateUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnUpdateScore -= updateUI;
    }

    private void updateUI(int score)
    {
        _scoreText.SetText("Score: " + score.ToString());
    }
}
