using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class TwinStickMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed= 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;

    public PlayerControls playerControls;
    private PlayerInput playerInput;

  

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
       
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();   
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
        isGamepad = playerInput.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
       
        
    }
    void HandleMovement()
    {
        //Vector3 move = new Vector3(movement.x, 0, movement.y);
        Vector3 move = transform.forward * movement.y + transform.right * movement.x;
        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    void HandleRotation()
    {
        if (isGamepad)
        {
            //rotate our player
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newrotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = newrotation * Quaternion.AngleAxis(-90, Vector3.up);

                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.magenta);
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }

        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

   





}
