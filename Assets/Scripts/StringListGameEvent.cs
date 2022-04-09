using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New StringListGameEvent", menuName = "ScriptableObjects/String List Game Event"), System.Serializable]
public class StringListGameEvent : ScriptableObject
{
    HashSet<StringListEventListener> listeners = new HashSet<StringListEventListener>();

    public virtual void Invoke(string[] o)
    {
        foreach (var globalEventListener in listeners)
            globalEventListener.RaiseEvent(o);
    }

    public virtual void Register(StringListEventListener gameEventListener) => listeners.Add(gameEventListener);

    public virtual void Deregister(StringListEventListener gameEventListener) => listeners.Remove(gameEventListener);
}
