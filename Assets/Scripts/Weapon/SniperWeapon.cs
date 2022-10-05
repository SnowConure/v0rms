using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class SniperWeapon : BaseWeapon
    {
        LineRenderer _lineRenderer;
        RaycastHit hit;

        public float KnockbackForce;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
        }
        public override void Aim(Vector2 mousePos)
        {
            _lineRenderer.enabled = true;

            Physics.Raycast(
                transform.position + -((transform.position - MousePosition()).normalized) * .7f,
                -(transform.position - (Vector3)mousePos).normalized,
                out hit,
                100
                );

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hit.transform ? hit.point : ((Vector3)mousePos - transform.position) * 100);
        }

        public override void AimRelease()
        {
            _lineRenderer.enabled = false;
             
        }


        public override void FireWeapon()
        {
            //Shoot
            if (!hit.transform) return;
            if (hit.transform.tag != "Worm") return;

            hit.transform.GetComponent<IDamageable>().TakeDamage(Damage, transform.parent, (MousePosition() - transform.position) * KnockbackForce + Vector3.up * 0.2f);
        }


    }
}