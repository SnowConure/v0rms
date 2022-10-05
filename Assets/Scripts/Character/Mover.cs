using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using System;

namespace Character
{
    public class Mover : MonoBehaviour
    {
        

        private float moveDirection;

        private bool canMove = false;
        public bool CanMove { get { return canMove; } set { canMove = value; if (!value) stopMove(); } }

        private Rigidbody rb;

        public float movementSpeed;
        public float jumpForce;

        [HideInInspector]
        public Animator anim;
        public GameObject model;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            
        }

        private void OnEnable()
        {
            GlobalGame.Instance.inputSystem.MovementDirection += UpdateMoveDirection;
            GlobalGame.Instance.inputSystem.Jump += Jump;
        }

    
        private void OnDisable()
        {
            GlobalGame.Instance.inputSystem.Jump -= Jump;
        }

        // Get player input and store in this class
        void UpdateMoveDirection(float _dir) => moveDirection = _dir;

        private void FixedUpdate()
        {
            if (canMove) Move();
        }

        void stopMove()
        {
            if (this == null) return;
            anim.SetFloat("velocity", 0);
        }

        // Do the movement
        void Move()
        {
            rb.MovePosition(transform.position + (Vector3.right * moveDirection) * movementSpeed * Time.deltaTime);

            if (moveDirection > 0) model.transform.localScale = new Vector3(1,1,-1);
            else if (moveDirection < 0) model.transform.localScale = new Vector3(1, 1, 1);

            anim.SetFloat("velocity", Mathf.Abs(moveDirection));


        }

        void Jump()
        {
            if (!canMove) return;
            if (GroundCheck()) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        bool GroundCheck()
        {
            if (Physics.Raycast(transform.position + Vector3.right * .49f, Vector3.down, 1f, ~1 >> 7)) return true;
            if (Physics.Raycast(transform.position + Vector3.left * .49f, Vector3.down, 1f, ~1 >> 7)) return true;


            return false;
        }

        
    }

}
