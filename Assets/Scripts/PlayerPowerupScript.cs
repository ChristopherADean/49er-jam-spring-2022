using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupScript : MonoBehaviour
{
    private PowerupScript equippedPowerup;
    private GameObject iconObj;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private DamageEventScriptable dEvent;
    private float powerupEndTime;
    private float lastAttackTime = -10f;
    

    private void Start()
    {
        powerupEndTime = equippedPowerup.duration + Time.time;

        iconObj = Instantiate(iconPrefab, GameObject.Find("PowerupDisplayContainer").transform);
        iconObj.GetComponent<PowerupUIItemScript>().Setup(equippedPowerup.pSprite, equippedPowerup.duration);


        switch (equippedPowerup.pType)
        {
            case (PowerupScript.PowerupTypes.PEPPER_SPRAY):

                break;
            case (PowerupScript.PowerupTypes.SPEED):

                break;
        }
    }


    private void Update()
    {
        if(Time.time < powerupEndTime)
        {
            switch (equippedPowerup.pType)
            {
                case (PowerupScript.PowerupTypes.PEPPER_SPRAY):

                    break;
                case (PowerupScript.PowerupTypes.SPEED):

                    break;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        if (iconObj != null)
            Destroy(iconObj);

        switch (equippedPowerup.pType)
        {
            case (PowerupScript.PowerupTypes.PEPPER_SPRAY):

                break;
            case (PowerupScript.PowerupTypes.SPEED):

                break;
        }
    }

    public void Setup(PowerupScript p)
    {
        equippedPowerup = p;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (equippedPowerup.pType)
        {
            case (PowerupScript.PowerupTypes.PEPPER_SPRAY):

                if (Time.time < lastAttackTime + equippedPowerup.attackCooldown)
                    return;

                DamageEventArgs dArgs = new DamageEventArgs();
                dArgs.damage = equippedPowerup.damage;
                dArgs.receivingObject = other.transform.root.gameObject;
                dArgs.upwardsVelocity = equippedPowerup.knockbackUp;
                dArgs.backwardsVelocity = equippedPowerup.knockbackBack;
                dArgs.attackDirection = Vector3.Normalize(other.transform.position - transform.position);

                dEvent.Invoke(dArgs);

                lastAttackTime = Time.time;
                    break;
            case (PowerupScript.PowerupTypes.SPEED):

                break;
        }
    }

}
