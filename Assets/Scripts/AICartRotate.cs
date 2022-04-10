using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICartRotate : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 r = rb.velocity.normalized;
        r.y = 0f;
        if(r != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(r);

    }
}
