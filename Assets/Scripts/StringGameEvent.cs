using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New StringGameEvent", menuName = "ScriptableObjects/String Game Event"), System.Serializable]
public class StringGameEvent : ScriptableObject
{
    HashSet<StringEventListener> listeners = new HashSet<StringEventListener>();

    public virtual void Invoke(string o)
    {
        foreach (var globalEventListener in listeners)
            globalEventListener.RaiseEvent(o);
    }

    public virtual void Register(StringEventListener gameEventListener) => listeners.Add(gameEventListener);

    public virtual void Deregister(StringEventListener gameEventListener) => listeners.Remove(gameEventListener);
}
