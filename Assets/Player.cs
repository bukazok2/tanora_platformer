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


    void Start()
    {
        this.inputActions = new PlayerInput();
        this.inputActions.Player.Enable();
        this.inputActions.Player.Jump.performed += Jump_performed;
        this.inputActions.Player.Movement.performed += Movement_performed;

        this.characterController = this.GetComponent<CharacterController>();
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log(inputVector);
        
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }

    private void Update()
    {
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
}
