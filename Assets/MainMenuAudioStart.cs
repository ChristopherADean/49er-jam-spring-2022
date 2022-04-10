using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManagerScript>().Play("bgm_main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
