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
    private Vector2 inputVector;

    [Header("Boundaries")]
    [SerializeField] private float zLimits;
    [SerializeField] private float xLeftLimit;
    [SerializeField] private float xRightLimit;

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
        inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    private void Movement()
    {
        if (Mathf.Abs(inputVector.y) > 0)
        {
            float increment = inputVector.y * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.z + increment, -maxSpeed, maxSpeed);
            rb.velocity = new(rb.velocity.x, 0f, newSpeed);
        }

        if (Mathf.Abs(inputVector.x) > 0)
        {
            float increment = inputVector.x * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -maxSpeed, maxSpeed);
            rb.velocity = new(newSpeed, 0f, rb.velocity.z);
        }

        rb.position = new(Mathf.Clamp(rb.position.x, xLeftLimit, xRightLimit), 0f, Mathf.Clamp(rb.position.z, -zLimits, zLimits));
    }

    private void VisualTilt()
    {
        float zRotation = rb.velocity.z / tiltAngleCompensation;

        rb.rotation = Quaternion.Euler(0f, 90f, zRotation);
    }

    private void ApplyDrag()
    {
        Vector3 vel = rb.velocity;

        if (inputVector.x == 0)
            vel.x *= drag;

        if (inputVector.y == 0)
            vel.z *= drag;

        rb.velocity = vel;
    }
}
