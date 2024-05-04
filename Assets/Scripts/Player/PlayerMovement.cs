using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    private Rigidbody rb;

    [Header("Drag")]
    [SerializeField] private float drag;

    [Header("Inputs")]
    PlayerInputs playerInputActions;
    private float inputVector;

    [Header("Boundaries")]
    [SerializeField] private float zLimits;

    [Header("Tilt angle")]
    [SerializeField] private float tiltAngleCompensation;

    private void Awake()
    {
        playerInputActions = new PlayerInputs();
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Inputs();
        VisualTilt();
    }

    private void FixedUpdate()
    {
        Movement();
        ApplyDrag();
    }

    private void Inputs()
    {
        inputVector = playerInputActions.Player.Movement.ReadValue<float>();
    }

    private void Movement()
    {
        if (Mathf.Abs(inputVector) > 0)
        {
            float increment = inputVector * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.z + increment, -maxSpeed, maxSpeed);
            rb.velocity = new Vector3(0f, 0f, newSpeed);
        }

        rb.position = new(0f, 0f, Mathf.Clamp(rb.position.z, -zLimits, zLimits));
    }

    private void VisualTilt()
    {
        float zRotation = rb.velocity.z / tiltAngleCompensation;

        rb.rotation = Quaternion.Euler(0f, 90f, zRotation);
    }

    private void ApplyDrag()
    {
        if (inputVector == 0)
            rb.velocity *= drag;
    }
}
