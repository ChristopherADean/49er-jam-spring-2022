using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    [SerializeField] private Transform cart;
    [SerializeField] private Transform cartSpawn;
    [SerializeField] private LayerMask interactLayermask;
    [SerializeField] private float grabDistance = 2f;
    [SerializeField] private float throwSpeed = 5f;
    [SerializeField] private GameEventScriptable itemGrabbedEvent;
    private GameObject heldItem;
    private ItemScriptable.ItemType target;

    public void OnPickup()
    {
        //Throw item if we hit button and have an item already
        if (heldItem != null)
        {
            ThrowItem();
            return;
        }
            

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabDistance, interactLayermask))
        {
            if (hit.collider.transform.parent.gameObject.tag != "Item" )
                return;

            if (hit.collider.transform.parent.gameObject.GetComponent<ItemPickupScript>().grabbable == false)
                return;

            heldItem = hit.collider.transform.parent.gameObject;
            heldItem.transform.SetParent(cart.transform);
            heldItem.GetComponent<ItemPickupScript>().grabbable = false;
            heldItem.GetComponent<Rigidbody>().isKinematic = true;
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.transform.localRotation = Quaternion.identity;
            heldItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            heldItem.GetComponentInChildren<BoxCollider>().enabled = false;

        }

        
    }

    public void RecieveDamageEvent(DamageEventArgs args)
    {
        if (!args.receivingObject.Equals(transform.root.gameObject) || heldItem == null)
            return;

        ThrowItem();
    }

    void ThrowItem()
    {
        heldItem.transform.SetParent(null);
        heldItem.transform.position = cartSpawn.transform.position;
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        Vector3 direction = Random.insideUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0f, 1f);
        direction = direction.normalized;
        rb.isKinematic = false;
        rb.velocity = direction * throwSpeed;
        heldItem.GetComponent<ItemPickupScript>().grabbable = true;
        heldItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        heldItem.GetComponentInChildren<BoxCollider>().enabled = true;
        heldItem = null;
    }

    public void ReceiveTriggerEnter(string s)
    {

        if (s != "Dropoff" || heldItem == null)
            return;

        //If this is the dropoff
        if(heldItem.GetComponent<ItemPickupScript>().associatedItem.iType == target)
        {
            Destroy(heldItem);
            heldItem = null;
            itemGrabbedEvent.Invoke(0f);
        }
    }

    public void RecieveNewTarget(float f)
    {
        target = (ItemScriptable.ItemType)f;
    }
}
