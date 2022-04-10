using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{

    private float time = 0f;
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0f)
            time = 0f;

        timerText.text = time.ToString("F1") + " Seconds";
    }

    public void SetTimer(float t)
    {
        time = t;
    }
}
