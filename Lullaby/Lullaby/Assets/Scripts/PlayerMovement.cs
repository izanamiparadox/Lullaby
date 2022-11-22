using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;

    PlayerStatus playerStats;

    public float moveSpeed;

    public CharacterController controller;

    public Transform mainCamera;
    public bool canRun;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public StaminaSystem staminaS;

    [SerializeField] float gravity;
    [SerializeField] Vector3 moveDirection = Vector3.zero;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerStats = GetComponent<PlayerStatus>();
        controller = GetComponent<CharacterController>();
        staminaS = FindObjectOfType<StaminaSystem>();

        playerInput.Player.Enable();

    }

    private void Update()
    {
        HandleInteract();
        HandleFire();
        HandleExit();
        HandleDrop();
        HandleSprint();
        HandleMovement();
        UpdateStats();
    }

    public void UpdateStats()
    {
        moveSpeed = playerStats.moveSpeed;
    }

    public void HandleMovement()
    {
        Vector2 inputVector = playerInput.Player.Walk.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 down = transform.TransformDirection(Vector3.down);

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * inputVector.x) + (down * inputVector.y);
        

        if (playerStats.canMove)
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


                if (playerStats.isRunning)
                {
                    canRun = true;

                    if (canRun && !staminaS.cD)
                    { 
                        controller.Move(moveDir.normalized * (moveSpeed * 2f) * Time.deltaTime);
                    }
                    else
                    {
                        controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
                    }

                }
                else
                {
                    canRun = false;
                    controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
                }

            }
            if (!controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            controller.Move(moveDirection * Time.deltaTime);
        }

    }


    public void HandleInteract()
    {
        var inputButton = playerInput.Player;

        if (inputButton.Interact.triggered && playerStats.canInteract)
        {
            playerStats.isInteracting = true;
        }
    }

    public void HandleFire()
    {
        var inputButton = playerInput.Player;

        inputButton.Fire.performed += SetFire;
        inputButton.Fire.canceled += SetFire;
    }

    public void HandleExit()
    {
        var inputButton = playerInput.Player;

        if (inputButton.Exit.triggered)
        {
            playerStats.isInteracting = false;
        }
    }

    public void HandleSprint()
    {
        var inputButton = playerInput.Player;

        inputButton.Sprint.performed += SetRun;
        inputButton.Sprint.canceled += SetRun;
    }

    public void HandleDrop()
    {
        var inputButton = playerInput.Player;

        inputButton.Drop.performed += SetDrop;
        inputButton.Drop.canceled += SetDrop;
    }

    public void SetRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerStats.isRunning = true;
        }
        else if (context.canceled)
        {
            playerStats.isRunning = false;
        }
        
    }

    public void SetDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerStats.isDroppingInventory = true;
        }
        else if (context.canceled)
        {
            playerStats.isDroppingInventory = false;
        }

    }

    public void SetFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerStats.settedFire = true;
        }
        else if (context.canceled)
        {
            playerStats.settedFire = false;
        }

    }
}
