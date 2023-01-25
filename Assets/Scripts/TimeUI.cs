using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    private TextMeshProUGUI _timeText;

    private void Awake()
    {
        _timeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnUpdateTime += updateUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnUpdateTime -= updateUI;
    }

    private void updateUI(float time)
    {
        _timeText.SetText("Time: " + time.ToString("F1"));
    }
}
