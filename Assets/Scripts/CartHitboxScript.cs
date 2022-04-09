using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartHitboxScript : MonoBehaviour
{
    private float hitBoxCanHitTime = -10f;

    [SerializeField] private float collideAbilityCooldown = 0.1f;  //Basically this makes it so the attack can only hit again after elapsed
    [SerializeField] private LayerMask collideAbleLayers;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float upwardsVelocity = 3f;
    [SerializeField] private float forwardsVelocity = 5f;
    [SerializeField] private DamageEventScriptable dEvent;

    private void OnTriggerEnter(Collider other)
    {
        //If not something to collide with ignore it
        if (collideAbleLayers ==  (collideAbleLayers | ( 1 << other.gameObject.layer)))
            return;
        //If hitbox cannot hit them ignore collission
        if (Time.time < hitBoxCanHitTime)
            return;

        Debug.Log("Hit Something");

        hitBoxCanHitTime = Time.time + collideAbilityCooldown;


        DamageEventArgs dArgs = new DamageEventArgs();
        dArgs.damage = damage;
        dArgs.upwardsVelocity = upwardsVelocity;
        dArgs.backwardsVelocity = forwardsVelocity;
        dArgs.attackDirection = transform.forward;
        dArgs.receivingObject = other.transform.root.gameObject;
    }
}
