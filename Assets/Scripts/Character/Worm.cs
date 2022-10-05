using System;
using UnityEngine;
using Weapon;


namespace Character
{
    public class Worm : MonoBehaviour, IDamageable, IEquip
    {
        Mover mover;
        Rigidbody rb;

        Vector3 startPosition;

        bool isMyTurn = false;

        public Action<Worm> GotClicked;
        public Action<Worm> Died;

        private BaseWeapon weapon;

        public GameObject SniperPrefab;

        public int Health;
        private int currentHealth;
        public HealthBar Healthbar;

        public float moveTime = 10;

        public float currentMoveTime = 0;

        public GameObject model;
        public GameObject modelPrefab;
        Animator anim;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            rb = GetComponent<Rigidbody>();

            currentHealth = Health;

            Debug.LogWarning("Temporarily set weapon to sniper");
            SetWeapon(SniperPrefab);


            GlobalGame.Instance.inputSystem.Moving += Moving;

         
        }

        private void Start()
        {
            model = Instantiate(modelPrefab, transform);
            anim = model.GetComponent<Animator>();
            mover.anim = anim;
            mover.model = model;
        }

        private void OnEnable()
        {
        }

        private void Moving()
        {
            if (!isMyTurn) return;
            currentMoveTime += Time.deltaTime;

            if (currentMoveTime >= moveTime) mover.CanMove = false;

            Healthbar.UpdateMove(1 - (currentMoveTime / moveTime));
        }

        public void Activate(bool state)
        {
            isMyTurn = state;
            mover.CanMove = state;
            if(!state) weapon.ReleaseAim();
            weapon.enabled = state;
            if (state) CameraController.main.SetFocus(transform);

            
            //Healthbar.HideAmmoUI(state);


        }

        public void NewTurn()
        {
            weapon.Reload();

            currentMoveTime = 0;
            startPosition = transform.position;
            //Healthbar.HideAmmoUI(false);
            Healthbar.UpdateMove(1 - (currentMoveTime / moveTime));
        }




      /*  private void Update()
        {
            if (!isMyTurn) return;
            
            // move distance check
            currentDistFormStart = Vector3.Distance(transform.position, startPosition);
            forceFieldMat.SetFloat("_Visibility", Mathf.InverseLerp(maxMoveDistance - 2, maxMoveDistance, currentDistFormStart)); 
            if (currentDistFormStart > maxMoveDistance)
            {
                rb.AddForce(-(transform.position - startPosition) * .3f, ForceMode.Impulse);
            }
        }*/

        public void SetWeapon(GameObject weaponPrefab)
        {
            if (weapon) Destroy(weapon.gameObject);

            weapon = Instantiate(weaponPrefab, transform).GetComponent<BaseWeapon>();
            weapon.enabled = isMyTurn;
            weapon.Reload();
        }

        private void OnMouseDown()
        {
            
            GotClicked?.Invoke(this);
        }

        public void TakeDamage(int damage, Transform dealer, Vector3 knockback)
        {
            currentHealth -= damage;

            anim.Play("Reaction");


            if (currentHealth <= 0) { Die(); return; }

            CameraController.main.QuickFocus(transform, 1);
            // Update HealthBar
            Healthbar.UpdateHealth((float)currentHealth / (float)Health);

            rb.AddForce(knockback, ForceMode.Impulse);
        }

        public void TakeDamage(int damage, Transform dealer, float knockback)
        {
            currentHealth -= damage;
            anim.Play("Reaction");



            if (currentHealth <= 0) { Die(); return; }

            CameraController.main.QuickFocus(transform, 1);
            // Update HealthBar
            Healthbar.UpdateHealth((float)currentHealth / (float)Health);

            rb.AddForce((transform.position - dealer.position).normalized * knockback, ForceMode.Impulse);
        }

        private void Die()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }

        public void Equip(GameObject equipable)
        {
            if (equipable.tag == "Weapon")
                SetWeapon(equipable);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            anim.Play("Reaction");

            if (currentHealth <= 0) Die();
        }

        public void Heal(float healAmount)
        {
            currentHealth += (int)healAmount;
            if (currentHealth > Health)
                currentHealth = Health;
        }

        public void Dance()
        {
            Healthbar.gameObject.SetActive(false);
            transform.eulerAngles = new Vector3(0, -90, 0);
            transform.localScale = new Vector3(1, 1, 1);
            anim.Play("Dance" + UnityEngine.Random.Range(1,6));
        }
    }

}
