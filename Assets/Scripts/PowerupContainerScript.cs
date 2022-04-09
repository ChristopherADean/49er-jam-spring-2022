using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupContainerScript : MonoBehaviour
{
    [SerializeField] GameObject powerupInventoryObject;
    [SerializeField] Transform inventoryTrans;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Powerup")
            return;

        PowerupScript p = other.GetComponent<SpinningItemScript>().GetAssociatedItem();
        GameObject g = Instantiate(powerupInventoryObject, inventoryTrans);
        g.GetComponent<PlayerPowerupScript>().Setup(p);
        Destroy(other.gameObject);
    }
}
