using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [HideInInspector]
    public float BlastRadius;
    [HideInInspector]
    public int Damage;
    [HideInInspector]
    public float KnockbackForce;
    public GameObject VFX;

    Vector3 startpos;

    public void Instantiated(float blastRadius, int damage, float knockbackForce)
    {
        startpos = transform.position;
        BlastRadius = blastRadius;
        Damage = damage;
        KnockbackForce = knockbackForce;
        GetComponent<SphereCollider>().enabled = false;

    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(startpos, transform.position) > 1.2f)
            GetComponent<SphereCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        GameObject vfx = Instantiate(VFX, transform.position, Quaternion.identity);
        CameraController.main.QuickFocus(vfx.transform, 2);
        RaycastHit[] hits = Physics.SphereCastAll(collision.GetContact(0).point, BlastRadius, Vector3.up);
        
        foreach (var hit in hits)
        {
            

            if (hit.transform.tag == "Worm")
            {
                RaycastHit s;
                Debug.DrawRay(collision.GetContact(0).point, -(collision.GetContact(0).point - hit.transform.position).normalized, Color.red, 2);
                if (!Physics.Raycast(collision.GetContact(0).point, -(collision.GetContact(0).point - hit.transform.position).normalized,out s, Vector3.Distance(collision.GetContact(0).point, hit.transform.position)-0.2f, 51))
                    hit.transform.GetComponent<IDamageable>().TakeDamage(Damage, transform, KnockbackForce);
            }

        }

    }
}
