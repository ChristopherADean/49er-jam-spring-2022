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

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.velocity.normalized), Time.deltaTime * 40f);

    }
}
