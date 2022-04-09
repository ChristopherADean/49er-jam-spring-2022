using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyFloatEvent : UnityEvent<float>
{

}

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEventScriptable gEvent;
    [SerializeField] MyFloatEvent uEvent;

    void Awake() => gEvent.Register(this);

    void OnDestroy() => gEvent.Deregister(this);

    public void RaiseEvent(float o) => uEvent.Invoke(o);
}
