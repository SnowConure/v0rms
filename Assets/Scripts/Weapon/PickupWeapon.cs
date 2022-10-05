using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class PickupWeapon : PickUp
{
    public GameObject PickUpObject;
    public GameObject Pivot;

    private void Start()
    {
        Instantiate(PickUpObject.GetComponent<BaseWeapon>().Model, Pivot.transform);
    }

    
    public override void PickedUp(GameObject player)
    {
        player.GetComponent<IEquip>().Equip(PickUpObject);
    }
}
