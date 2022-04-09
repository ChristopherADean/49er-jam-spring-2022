using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyDamageEvent : UnityEvent<DamageEventArgs>
{

}

public class DamageEventListener : MonoBehaviour
{
    [SerializeField] DamageEventScriptable gEvent;
    [SerializeField] MyDamageEvent uEvent;

    void Awake() => gEvent.Register(this);

    void OnDestroy() => gEvent.Deregister(this);

    public void RaiseEvent(DamageEventArgs o) => uEvent.Invoke(o);
}

public struct DamageEventArgs
{
    public float damage;
    public float upwardsVelocity;
    public float backwardsVelocity;
    public Vector3 attackDirection;
    public GameObject receivingObject;
}
