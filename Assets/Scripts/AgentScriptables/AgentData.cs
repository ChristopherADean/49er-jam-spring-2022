using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AgentData", menuName = "ScriptableObjects/AgentData"), System.Serializable]
public class AgentData : ScriptableObject
{
    public string aName = "Default Agent";
    public float health = 3f;
    public float checkFrequency = 3f;
    public float pickupRange = 2f;
    public float shortStunTime = 0.5f;
    public float longStunTime = 10f;
}
