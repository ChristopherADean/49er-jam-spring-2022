using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private TextMeshProUGUI sText;

    private void Start()
    {
        sText = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateScore(float f)
    {

        sText.text = "Score: " + (int)f;
    }
}
