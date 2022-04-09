using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New GameEvent", menuName = "ScriptableObjects/Game Event"), System.Serializable]
public class GameEventScriptable : ScriptableObject
{
    HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

    public virtual void Invoke(float o)
    {
        foreach (var globalEventListener in listeners)
            globalEventListener.RaiseEvent(o);
    }

    public virtual void Register(GameEventListener gameEventListener) => listeners.Add(gameEventListener);

    public virtual void Deregister(GameEventListener gameEventListener) => listeners.Remove(gameEventListener);
}
