using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboardScript : MonoBehaviour
{
    private Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.transform != null)
        {
            mainCam = Camera.main.transform;
            Vector3 lookPos = mainCam.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos, Vector3.up);
            transform.Rotate(0, 180, 0);
        }
        
    }
}
