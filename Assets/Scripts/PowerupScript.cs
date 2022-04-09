using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "ScriptableObjects/Powerup"), System.Serializable]
public class PowerupScript : ScriptableObject
{
    public string pName = "default powerup";
    public Sprite pSprite;
    public PowerupTypes pType;
    public float duration = 10f;
    public float damage = 1f;
    public float attackCooldown = 0.1f;
    public float knockbackUp = 3f;
    public float knockbackBack = 5f;
    public float speedMult = 1.8f;

    public enum PowerupTypes
    {
        PEPPER_SPRAY,
        SPEED
    }
}
