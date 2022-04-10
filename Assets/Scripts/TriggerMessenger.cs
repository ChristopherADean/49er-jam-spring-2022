using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessenger : MonoBehaviour
{
    [SerializeField] private StringGameEvent tEvent;
    private void OnTriggerEnter(Collider other)
    {
        tEvent.Invoke(other.tag);
    }
}
