using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item"), System.Serializable]
public class ItemScriptable : ScriptableObject
{
    public string pName = "default item";
    public Sprite icon;
    public ItemType iType;


    public enum ItemType
    {
        Bootlego,
        Fettuc,
        GrannySmith,
        JimBlender,
        Michaelsoft,
        Oof,
        Poochie,
        PTSD,
        Soomsang,
        Tomato,
        TradingCards
    }
}
