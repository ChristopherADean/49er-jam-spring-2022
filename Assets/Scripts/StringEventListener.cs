using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyStringEvent : UnityEvent<string>
{

}

public class StringEventListener : MonoBehaviour
{
    [SerializeField] StringGameEvent gEvent;
    [SerializeField] MyStringEvent uEvent;

    void Awake() => gEvent.Register(this);

    void OnDestroy() => gEvent.Deregister(this);

    public void RaiseEvent(string o) => uEvent.Invoke(o);
}
