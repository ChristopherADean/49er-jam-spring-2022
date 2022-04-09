using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboardScript : MonoBehaviour
{
    private Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = mainCam.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos, Vector3.up);
    }
}
