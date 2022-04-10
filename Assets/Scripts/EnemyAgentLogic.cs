using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentLogic : MonoBehaviour
{
    private GameObject targetItem;
    private GameObject heldItem;
    private NavMeshAgent agent;
    private float unStunTime = 0f;
    private AIState aState = AIState.HEADING_TO_TARGET;
    [SerializeField] private AgentData aData;
    [SerializeField] private Transform cart;
    [SerializeField] private Transform cartSpawn;
    private float health = 3f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        health = aData.health;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        SelectTargetItem();

        StartCoroutine(CheckSurroundings(aData.checkFrequency));
    }

    // Update is called once per frame
    void Update()
    {  
        //If we are stunned and the stun cooldown is over find a target and head to it
        if(unStunTime < Time.time && aState == AIState.STUNNED)
        {
            agent.enabled = true;
            SelectTargetItem();
            aState = AIState.HEADING_TO_TARGET;
            
        }



        if (Vector3.Distance(transform.position, targetItem.transform.position) < aData.pickupRange && heldItem == null && heldItem.GetComponent<ItemPickupScript>().grabbable == true)
        {
            heldItem = targetItem;
            heldItem.transform.SetParent(cart.transform);
            heldItem.GetComponent<ItemPickupScript>().grabbable = false;
            heldItem.GetComponent<Rigidbody>().isKinematic = true;
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.transform.localRotation = Quaternion.identity;
            heldItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            heldItem.GetComponentInChildren<BoxCollider>().enabled = false;

            aState = AIState.HEADING_TO_EXIT;
        }

    }

    private void SelectTargetItem()
    {
        //Get list of items
        GameObject[] availableItems = GameObject.FindGameObjectsWithTag("Item");
        //Select one randomly
        int index = (int) Random.Range(0f, availableItems.Length - 1f);
        targetItem = availableItems[index];

        //Set the agent's destination to the location
        agent.destination = targetItem.transform.position;
    }

    private IEnumerator CheckSurroundings(float time)
    {
        yield return new WaitForSeconds(time);
        



        StartCoroutine(CheckSurroundings(aData.checkFrequency));
    }

    public void ReceiveDamageEvent(DamageEventArgs args)
    {
        if (!args.receivingObject.Equals(gameObject))
            return;

        //Subtract from health
        health -= args.damage;
        CheckHealth();

        //Set velocity based on knockback variables
        rb.velocity = new Vector3(0f, args.upwardsVelocity, 0f) + (args.backwardsVelocity * args.attackDirection);


        //If we get damaged set state to stun, set a time when we get unstunned. 
        aState = AIState.STUNNED;
        unStunTime = Time.time + aData.shortStunTime;
        agent.enabled = false;
        ThrowItem();

    }

    void CheckHealth()
    {
        
    }

    void ThrowItem()
    {
        if (heldItem == null)
            return;

        heldItem.transform.SetParent(null);
        heldItem.transform.position = cartSpawn.transform.position;
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        Vector3 direction = Random.insideUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0f, 1f);
        direction = direction.normalized;
        rb.isKinematic = false;
        rb.velocity = direction * 5f;
        heldItem.GetComponent<ItemPickupScript>().grabbable = true;
        heldItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        heldItem.GetComponentInChildren<BoxCollider>().enabled = true;
        heldItem = null;
    }

    public enum AIState 
    {
        HEADING_TO_TARGET,
        HEADING_TO_EXIT,
        ATTACKING_TARGET,
        PICKING_UP_ITEM,
        STUNNED
    }
}
