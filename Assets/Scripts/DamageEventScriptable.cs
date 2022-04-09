using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New DamageEvent", menuName = "ScriptableObjects/Damage Event"), System.Serializable]
public class DamageEventScriptable : ScriptableObject
{
    HashSet<DamageEventListener> listeners = new HashSet<DamageEventListener>();

    public virtual void Invoke(DamageEventArgs o)
    {
        foreach (var globalEventListener in listeners)
            globalEventListener.RaiseEvent(o);
    }

    public virtual void Register(DamageEventListener gameEventListener) => listeners.Add(gameEventListener);

    public virtual void Deregister(DamageEventListener gameEventListener) => listeners.Remove(gameEventListener);
}
