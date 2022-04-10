using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScore : MonoBehaviour
{
    [SerializeField] private MainGameManager gm;
    [SerializeField] private TextMeshProUGUI sText;


    private void OnEnable()
    {
        sText.text = "Score: " + gm.score.ToString("F0");
    }
}
