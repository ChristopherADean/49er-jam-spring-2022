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
    private GameObject heldItem;

    public void OnPickup()
    {
        if (heldItem != null)
            ThrowItem();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabDistance, interactLayermask))
        {

            if (hit.collider.transform.root.gameObject.tag != "Item" || hit.collider.transform.root.gameObject.GetComponent<ItemPickupScript>().grabbable == false)
                return;

            heldItem = hit.collider.transform.root.gameObject;
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
}
