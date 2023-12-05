#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Game Objects")]
        [SerializeField] private CharacterController character;
        [SerializeField] private Camera lookCamera;
        [SerializeField] private Transform? groundCheck;
        [SerializeField] private Animator animator;
        [Header("Settings")]
        [SerializeField] private PlayerPreferences preferences;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Settings settings;
        // [SerializeField] private float moveSpeed = 20.0f;
        // [SerializeField] private float dashSpeed = 20.0f;
        [SerializeField] private float jumpSpeed = 20f;
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private float gravityMultiplier = 1.0f;
        [SerializeField] private float kiteMultiplier = 0.5f;
        [SerializeField] private LayerMask groundMask;

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction fireAction;
        private InputAction jumpAction;

        private bool isDashing = false;
        private bool isGrounded = true;
        private float verticalVelocity = 0;
        private Vector3 dashDirection = Vector3.zero;
        private float xRotation = 0f;
        private bool doubleJumpAllowed = true;
        private bool isKiting = false;

        void Awake()
        {
            character ??= GetComponent<CharacterController>();
            playerInput ??= GetComponent<PlayerInput>();

            moveAction = playerInput.actions["Move"];
            lookAction = playerInput.actions["Look"];
            fireAction = playerInput.actions["Fire"];
            jumpAction = playerInput.actions["Jump"];
        }

        public void UpdateBehaviour()
        {
            if (Game.Instance.State.Playing)
            {
                ApplyGravity();
                Look();
                Movement();
                Animate();
            }
        }

        private void Animate()
        {
            animator.SetFloat("speed", character.velocity.magnitude);
        }

        public bool ShouldPlayerBeDead()
        {
            return this.transform.position.y <= settings.deadY;
        }

        private void ApplyGravity()
        {
            if (character.isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = -1.0f;
            }
            else 
            {
                verticalVelocity += Physics.gravity.y * (isKiting ? kiteMultiplier : gravityMultiplier) * Time.deltaTime;
            }
            
        }

        // private bool IsGrounded()
        // {
        //     return groundCheck != null ? Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) : false;
        // }

        private void Movement()
        {
            var direction = CalcMoveDirection();
            var jumpFlag = jumpAction.triggered;
            var isGrounded = character.isGrounded; // IsGrounded();

            if (isGrounded)
            {
                doubleJumpAllowed = true;
                isKiting = false;
            }

            if (jumpFlag && isGrounded)
            {
                verticalVelocity = Mathf.Sqrt(jumpSpeed * Physics.gravity.y * -2);
            }

            if(jumpFlag && isKiting)
            {
                isKiting = false;
            }

            if (jumpFlag && !isGrounded && doubleJumpAllowed)
            {
                verticalVelocity = Mathf.Sqrt(jumpSpeed * Physics.gravity.y * -2);
                doubleJumpAllowed = false;
                isKiting = true;
            }

            var move = (direction * settings.moveSpeed)
                       + (transform.up * verticalVelocity)
                       + (isDashing ? dashDirection * settings.dashSpeed : Vector3.zero);

            character?.Move(move * Time.deltaTime);
        }

        private Vector3 CalcMoveDirection()
        {
            Vector2 inputDirection = moveAction.ReadValue<Vector2>();
            return Vector3.ClampMagnitude((transform.right * inputDirection.x) + (transform.forward * inputDirection.y), 1);
        }

        private void Look()
        {
            var lookDirection = lookAction.ReadValue<Vector2>();
            var mouse = lookDirection * Time.deltaTime * preferences.mouseSensitivity;

            this.transform.Rotate(Vector3.up * mouse.x);

            this.xRotation -= mouse.y;
            this.xRotation = Mathf.Clamp(this.xRotation, settings.minYAngle, settings.maxYAngle);

            if (lookCamera != null)
            {
                lookCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            }
        }
    }
}

