using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public static event Action<Player> OnPlayerSpawned = delegate { };
    public static event Action<Player> OnPlayerJump = delegate { };

    private CharacterController characterController;
    private PlayerInput inputActions;

    // player
    private float currentSpeed;
    // we need some offset to check if the max speed reached or not
    private float speedOffset = 0.1f;
    // Is player pressed jump action or not
    private bool isJumping = false;
    // Is player grounded we constantly checking it
    private bool isGrounded = true;
    // just to store the vertical velocity for Move method
    private float verticalVelocity;


    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    [SerializeField] float moveSpeed;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] float speedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    [SerializeField] float jumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    [SerializeField] float gravity = -15.0f;

    [Tooltip("Useful for rough ground")]
    [SerializeField] float groundedOffset = 0.08f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    [SerializeField] float groundedRadius = 1.2f;

    [Tooltip("What layers the character uses as ground")]
    [SerializeField] LayerMask groundLayers;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    [SerializeField] float jumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    [SerializeField] float fallTimeout = 0.15f;

    [Tooltip("TerminalVelocity for gravity")]
    [SerializeField] float terminalVelocity = 53.0f;

    int score = 0;

    public void AddScore(int score)
    {
        this.score += score;
        Debug.Log(this.score);
    }

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        this.characterController = this.GetComponent<CharacterController>();
        this.inputActions = new PlayerInput();
        this.inputActions.Player.Enable();
        this.inputActions.Player.Jump.performed += Jump_performed;
        this.inputActions.Player.Movement.performed += Movement_performed;

        this.jumpTimeoutDelta = jumpTimeout;
        this.fallTimeoutDelta = fallTimeout;

        OnPlayerSpawned?.Invoke(this);
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        // Debug.Log(inputVector);
        // de jelenleg ez csak 1 gomb nyomás , tehát nyomogatni kellene ,
        // hogy mozogjon a player , ehelyett az update-bõl olvassuk ki
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        this.isJumping = true;
        Debug.Log("Jumping : " + this.isJumping);
    }

    private void Update()
    {
        this.JumpAndGravity();
        this.GroundedCheck();
        this.Move();
    }

    private void Move()
    {
        Vector2 inputVector = this.inputActions.Player.Movement.ReadValue<Vector2>();

        float targetSpeed = this.moveSpeed;

        if (inputVector == Vector2.zero)
            targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            currentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);

            currentSpeed = Mathf.Round(currentSpeed * 1000f) / 1000f;
        }
        else
        {
            currentSpeed = targetSpeed;
        }

        Vector3 targetDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);
        // move the player
        characterController.Move(targetDirection * (currentSpeed * Time.deltaTime) +
                             new Vector3(0.0f, this.verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
            transform.position.z);
        this.isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
            QueryTriggerInteraction.Ignore);

        //Debug.Log(this.isGrounded);
    }

    private void JumpAndGravity()
    {
        if (this.isGrounded)
        {
            // reset the fall timeout timer
            fallTimeoutDelta = fallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (this.verticalVelocity < 0.0f)
            {
                this.verticalVelocity = -2f;
            }

            if (this.isJumping && jumpTimeoutDelta <= 0.0f)
            {
                OnPlayerJump?.Invoke(this);
                this.verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * this.gravity);
            }

            if (jumpTimeoutDelta >= 0.0f)
            {
                this.jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            this.jumpTimeoutDelta = jumpTimeout;

            if (this.fallTimeoutDelta >= 0.0f)
            {
                this.fallTimeoutDelta -= Time.deltaTime;
            }

            this.isJumping = false;
        }

        if (this.verticalVelocity < this.terminalVelocity)
        {
            this.verticalVelocity += this.gravity * Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (this.isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
            groundedRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable ic = other.gameObject.GetComponent<ICollectable>();
        if (ic != null)
        {
            // event fire
            ic.Collect();
        }
    }
}
