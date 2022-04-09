using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AgentData", menuName = "ScriptableObjects/AgentData"), System.Serializable]
public class AgentData : ScriptableObject
{
    public string aName = "Default Agent";
    public float checkFrequency = 3f;
}
