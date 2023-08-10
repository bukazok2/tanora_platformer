using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speedChangeRate = 10f;
    [SerializeField] float moveSpeed = 5.0f;

    [SerializeField] float speedOffset = 0.1f;

    private float currentSpeed;
    private float verticalVelocity;
    private CharacterController characterController;
    private PlayerInput inputActions;
    private bool isGrounded = true;
    private float fallTimeoutDelta;
    private float fallTimeout = 0.15f;
    private bool isJumping;
    private float jumpTimeoutDelta ;
    private float gravity = -15f;

    [Tooltip("Tooltip")]
    [SerializeField] float groundOffset = 0.08f;
    [SerializeField] float groundedRadius = 1.2f;
    [Space(10)]
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float jumpTimeout = 0.50f;
    [SerializeField] float terminalVelocity = 53.0f;

    void Start()
    {
        this.inputActions = new PlayerInput();
        this.inputActions.Player.Enable();
        this.inputActions.Player.Jump.performed += Jump_performed;
        this.inputActions.Player.Movement.performed += Movement_performed;

        this.characterController = this.GetComponent<CharacterController>();

        Global.score++;
        Debug.Log(Global.score);
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        GamePlayHandler.Instance.Init();
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        this.isJumping = true;
        Debug.Log("Jump");
    }

    private void Update()
    {
        this.JumpAndGravity();
        this.GroundCheck();
        this.Move();
    }

    private void Move()
    {
        Vector2 inputVector = this.inputActions.Player.Movement.ReadValue<Vector2>();

        float targetSpeed = this.moveSpeed;

        if (inputVector == Vector2.zero)
        {
            targetSpeed = 0.0f;
        }

        float currentHorizontalSpeed = new Vector3(this.characterController.velocity.x, 0.0f, this.characterController.velocity.z).magnitude;

        if (currentHorizontalSpeed < targetSpeed - this.speedOffset || currentHorizontalSpeed > targetSpeed + this.speedOffset)
        {
            this.currentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);

            this.currentSpeed = Mathf.Round(this.currentSpeed * 1000f) / 1000f;
        }
        else
        {
            this.currentSpeed = targetSpeed;
        }

        Vector3 targetDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        this.characterController.Move(targetDirection * (currentSpeed * Time.deltaTime) +
            new Vector3(0.0f, this.verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        this.isGrounded = Physics.CheckSphere(spherePosition, this.groundedRadius, this.groundLayers, QueryTriggerInteraction.Ignore);

        Debug.Log(isGrounded);
    }

    private void OnDrawGizmos()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (this.isGrounded)
            Gizmos.color = transparentGreen;
        else
            Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(this.transform.position.x, this.transform.position.y - this.groundOffset, this.transform.position.z), this.groundedRadius);
    }

    private void JumpAndGravity()
    {
        if (this.isGrounded)
        {
            // földön vagyunk de ugrani szeretnénk
            fallTimeoutDelta = fallTimeout;

            if(this.verticalVelocity < 0.0f)
            {
                this.verticalVelocity = -2f;
            }

            if(this.isJumping && jumpTimeoutDelta <= 0.0f)
            {
                this.verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * this.gravity);
            }

            if(jumpTimeoutDelta >= 0.0f)
            {
                this.jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // ugrottunk és földet érünk
            this.jumpTimeoutDelta = jumpTimeout;

            if(this.fallTimeoutDelta >= 0.0f)
            {
                this.fallTimeoutDelta -= Time.deltaTime;
            }

            this.isJumping = false;
        }

        if(this.verticalVelocity < this.terminalVelocity)
        {
            this.verticalVelocity += this.gravity * Time.deltaTime;
        }
    }


}
