using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    [SerializeField] private ItemScriptable associatedItem;
    public bool grabbable = true;
    public bool claimed = false;


}
