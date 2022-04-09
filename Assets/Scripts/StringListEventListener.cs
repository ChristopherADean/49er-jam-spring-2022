using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyStringListEvent : UnityEvent<string[]>
{

}

public class StringListEventListener : MonoBehaviour
{
    [SerializeField] StringListGameEvent gEvent;
    [SerializeField] MyStringListEvent uEvent;

    void Awake() => gEvent.Register(this);

    void OnDestroy() => gEvent.Deregister(this);

    public void RaiseEvent(string[] o) => uEvent.Invoke(o);
}
