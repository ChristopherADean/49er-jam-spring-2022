using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToBuyTextScript : MonoBehaviour
{

    private TextMeshProUGUI toBuyText;
    // Start is called before the first frame update
    void Start()
    {
        toBuyText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string s)
    {
        toBuyText.text = "- " + s;
    }
}
