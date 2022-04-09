using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerupUIItemScript : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float timerValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerValue -= Time.deltaTime;
        countdownText.text = timerValue.ToString("F1");
    }

    public void Setup(Sprite icon, float lifetime)
    {
        timerValue = lifetime;
        iconImage.sprite = icon;
    }
}
