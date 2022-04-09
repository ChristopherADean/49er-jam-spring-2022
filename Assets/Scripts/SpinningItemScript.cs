using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningItemScript : MonoBehaviour
{
    [SerializeField] float spinSpeed = 30f;
    [SerializeField] PowerupScript associatedItem;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0f, spinSpeed * Time.deltaTime, 0f);
    }

    public PowerupScript GetAssociatedItem()
    {
        return associatedItem;
    }
}
