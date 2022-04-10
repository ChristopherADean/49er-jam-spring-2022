using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAttack : MonoBehaviour
{
    //private vars
    private float attackStart = -10f;
    
    private Vector3 defaultPos;
    private BoxCollider hitBox;
    

    //Attack vars
    [SerializeField] private float attackDuration = 1f;
    [SerializeField] private float cancelTime = 0.3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float hitBoxActiveTime = 0.2f;
    [SerializeField] private float hitBoxDeActivateTime = 0.6f;
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private GameObject cart;


    private void Start()
    {
        defaultPos = cart.transform.localPosition;
        hitBox = cart.GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        if(Time.time < attackStart + attackDuration)
        {
            float zPos = (attackCurve.Evaluate((Time.time - attackStart) / attackDuration)*attackRange) + defaultPos.z;
            cart.transform.localPosition = new Vector3(defaultPos.x, defaultPos.y, zPos);

            if(Time.time - attackStart > hitBoxActiveTime && Time.time - attackStart < hitBoxDeActivateTime)
            {
                hitBox.enabled = true;
            }

            if(Time.time - attackStart > hitBoxDeActivateTime)
            {
                hitBox.enabled = false;
            }
        }
        else
        {
            hitBox.enabled = false;
            cart.transform.localPosition = defaultPos;
        }
    }

    public void OnAttack()
    {
        FindObjectOfType<AudioManagerScript>().Play("sfx_swing");
        if (Time.time > attackStart + cancelTime)
            attackStart = Time.time;
    }
}
