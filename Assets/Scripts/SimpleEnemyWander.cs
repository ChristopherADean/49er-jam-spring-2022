using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyWander : MonoBehaviour
{
    [SerializeField] private AgentData aData;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private float health;
    private float unStunTime = 0f;
    private bool stunned = true;

    // Start is called before the first frame update
    void Start()
    {
        health = aData.health;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(FindNewTarget(aData.checkFrequency));

    }

    private void FixedUpdate()
    {
        if(stunned && Time.time > unStunTime)
        {
            rb.isKinematic = true;
            stunned = false;
            agent.enabled = true;
            StartCoroutine(FindNewTarget(aData.checkFrequency));
        }
    }

    IEnumerator FindNewTarget(float time)
    {
        if(agent.enabled)
            agent.SetDestination(RandomNavSphere(Vector3.zero, aData.pathFindDistance, -1));
        yield return new WaitForSeconds(time);

        if(agent.enabled)
            StartCoroutine(FindNewTarget(aData.checkFrequency));
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void ReceiveDamageEvent(DamageEventArgs args)
    {

        Debug.Log("Got hit " +args.receivingObject);
        if (!args.receivingObject.Equals(gameObject))
            return;
        FindObjectOfType<AudioManagerScript>().Play("sfx_hit");
        FindObjectOfType<AudioManagerScript>().Play("sfx_grunt_m_" + Random.Range(0, 13));
        //Subtract from health
        health -= args.damage;
        stunned = true;
        CheckHealth();


        //Set velocity based on knockback variables
        rb.isKinematic = false;
        rb.velocity = new Vector3(0f, args.upwardsVelocity, 0f) + (args.backwardsVelocity * args.attackDirection);


        //If we get damaged set state to stun, set a time when we get unstunned. 
        
        

    }

    void CheckHealth()
    {
        agent.enabled = false;

        if(health > 0)
        {
            unStunTime = Time.time + aData.shortStunTime;
        }
        else
        {
            unStunTime = Time.time + aData.longStunTime;
        }

        
    }
}
