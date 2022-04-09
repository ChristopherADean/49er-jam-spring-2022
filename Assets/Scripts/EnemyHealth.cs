using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 3f;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ReceiveDamageEvent(DamageEventArgs args)
    {
        //Immediatelly return if not for us
        if (!args.receivingObject.Equals(gameObject))
            return;


        health -= args.damage;
        CheckHealth();

        //Set velocity based on knockback variables
        rb.velocity = new Vector3(0f, args.upwardsVelocity, 0f) + (args.backwardsVelocity * args.attackDirection);
    }

    private void CheckHealth()
    {

    }
}
