using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Worm")
        {
            PickedUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void PickedUp(GameObject player)
    {

    }
}
