using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreUI : MonoBehaviour
{
    private static HighscoreUI _instance;

    [SerializeField] private Transform _highscorePrefab;

    private int _numberOfHighscores;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _numberOfHighscores = (int)(transform.GetComponent<RectTransform>().rect.height / _highscorePrefab.GetComponent<RectTransform>().rect.height);

        Debug.Log(_numberOfHighscores);
    }

    public static void UpdateHighscores(List<Highscore> highscores)
    {
        _instance.updateHighscores(highscores);
    }

    private void updateHighscores(List<Highscore> highscores)
    {
        if (highscores == null)
            return;

        foreach (Transform item in transform)
            Destroy(item.gameObject);

        for (int i = 0; i < _numberOfHighscores; i++)
        {
            if (i >= highscores.Count)
                return;

            TextMeshProUGUI text = Instantiate(_highscorePrefab, transform).GetComponent<TextMeshProUGUI>();
            text.SetText(highscores[i].TotalScore.ToString("F1"));
            
        }
    }
}
