using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float force = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Worm")
        {
            Debug.Log(other.name);

            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
