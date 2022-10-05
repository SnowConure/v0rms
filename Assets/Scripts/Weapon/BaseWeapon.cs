using Character;
using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{


    public class BaseWeapon : MonoBehaviour
    {

        public GameObject Model;

        bool aiming = false;

        public int Damage;

        public int AmmoPerRound;
        private int currentAmmo;

        private void OnEnable()
        {
            GlobalGame.Instance.inputSystem.FireDown += Fire;
            GlobalGame.Instance.inputSystem.AimDown += ReadyWeapon;
            GlobalGame.Instance.inputSystem.AimUp += ReleaseAim;

        }
        private void OnDisable()
        {
            GlobalGame.Instance.inputSystem.FireDown -= Fire;
            GlobalGame.Instance.inputSystem.AimUp -= ReleaseAim;
            GlobalGame.Instance.inputSystem.AimDown -= ReadyWeapon;

        }

        private void ReadyWeapon() => StartCoroutine(FireHold());
        public void ReleaseAim() => aiming = false;
        private void Fire()
        {
            if (aiming == false) return;
            ReleaseAim();

            if (currentAmmo <= 0) return;
            
            currentAmmo--;

            transform.parent.GetComponent<Worm>().Healthbar.UpdateAmmo(currentAmmo);
            FireWeapon();
            
        }

        public void Reload ()
        {

            currentAmmo = AmmoPerRound;

            transform.parent.GetComponent<Worm>().Healthbar.UpdateAmmo(currentAmmo);

        }
        IEnumerator FireHold()
        {
            aiming = true;
            while(aiming)
            {
                Aim(MousePosition());
                yield return null;
            }
            AimRelease();
        }

        public virtual void AimRelease() { 
        }

        public virtual void Aim(Vector2 mousePos)
        {

        }

        public virtual void FireWeapon()
        {

        }


        public Vector3 MousePosition()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;

            if(mousePos.x > Screen.width) mousePos.x = Screen.width;
            if(mousePos.x < 0) mousePos.x = 0;
            if (mousePos.y > Screen.height) mousePos.y = Screen.height;
            if (mousePos.y < 0) mousePos.y = 0;
            Debug.Log(mousePos);
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}