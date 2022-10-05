using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public float stormSpeed;
    public float stormDecrement = 100;

    public float minScale;
    private void OnEnable()
    {

        TurnHandler.Instance.StartTurn += Shrink;
    }

    private void Shrink(int turn)
    {
        if (turn != 0) return;

        if (transform.localScale.x <= minScale)
        {
            TurnHandler.Instance.NextTurn();

            return; 
        }

        StartCoroutine(ShrinkStorm());
    }

    IEnumerator ShrinkStorm()
    {
        CameraController.main.QuickWideFocus();
        float finalScale = transform.localScale.x - stormDecrement;
        while (transform.localScale.x > finalScale)
        {
            if (transform.localScale.x <= minScale) finalScale = 10000000;
                transform.localScale -= (Vector3.one - Vector3.forward) * stormSpeed * Time.deltaTime;
            yield return null;

        }

        TurnHandler.Instance.NextTurn();


    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Worm")
        {
            other.GetComponent<IDamageable>().TakeDamage(1000);
        }
    }
}
