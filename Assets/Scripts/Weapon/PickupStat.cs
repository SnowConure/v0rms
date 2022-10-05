using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    MovementTime,
    Health,
    Jumpheight,
    Speed,
}

public class PickupStat : PickUp
{
    public Stats Stat;
    public float statValue;


    public override void PickedUp(GameObject player)
    {
       switch(Stat)
        {
            case Stats.MovementTime:
                player.GetComponent<Worm>().moveTime += statValue;
                player.GetComponent<Worm>().currentMoveTime += statValue;
                break;
            case Stats.Health:
                player.GetComponent<Worm>().Heal(statValue);
                break;
            case Stats.Jumpheight:
                player.GetComponent<Mover>().jumpForce += statValue;
                break;
            case Stats.Speed:
                player.GetComponent<Mover>().movementSpeed += statValue;
                break;
        }
    }
}
