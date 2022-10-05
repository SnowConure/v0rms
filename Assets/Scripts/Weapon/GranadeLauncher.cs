using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class GranadeLauncher : BaseWeapon
    {
        public float KnockbackForce;
        ArcRenderer _arcRenderer;

        public float BlastRadius;
        public GameObject Granade;

        public GameObject PreviewHit;

        private void Awake()
        {
            _arcRenderer = GetComponent<ArcRenderer>();
            PreviewHit.transform.localScale = Vector3.one * BlastRadius * 2;
        }

        public override void Aim(Vector2 mousePos)
        {

            PreviewHit.SetActive(true);
            PreviewHit.transform.position = _arcRenderer.RenderArc(-(transform.position - (Vector3)mousePos) * 2);
        }

        public override void AimRelease()
        {
            _arcRenderer.ClearArc();
            PreviewHit.SetActive(false);
        }


        public override void FireWeapon()
        {
            Rigidbody currentGranade = Instantiate(Granade, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            currentGranade.AddForce(-(transform.position - MousePosition()) * 2.85f, ForceMode.Impulse);
            currentGranade.GetComponent<Granade>().Instantiated(BlastRadius, Damage, KnockbackForce);
            CameraController.main.QuickFocus(currentGranade.transform);
        }
    }
}