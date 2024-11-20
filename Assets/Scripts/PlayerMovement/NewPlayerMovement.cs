using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 0.2f;
    [SerializeField] private float dashDuration = 0.2f;

    private float dashTimer;
    private float dashCooldownTimer;
    private bool isDashing;
    private float dirX;
    private float dirY;

    public float moveSpeed = 5f;
    public Animator animator;
    private GameInput gameInput;

    public Vector2 movement;

    private void Awake()
    {
        gameInput = new GameInput();
    }

    private void OnEnable()
    {
        gameInput.Enable();
        gameInput.Player.Dash.performed += OnDashPerformed;
    }

    private void OnDisable()
    {
        gameInput.Player.Dash.performed -= OnDashPerformed;
        gameInput.Disable();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        movement = gameInput.Player.Move.ReadValue<Vector2>();
        dirX = movement.x;
        dirY = movement.y;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private bool TryMove(Vector2 dir, float distance)
    {
        return Physics2D.Raycast(GetComponent<Rigidbody2D>().position, dir, distance);
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (dashCooldownTimer <= 0f)
        {
            StartDash();
        }
        else
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void StartDash()
    {
        dirX = movement.x;
        dirY = movement.y;

        Debug.Log("dashing");

        dashTimer = dashDuration;
        transform.position += new Vector3(dirX, dirY, 0).normalized * dashSpeed;
       
    }

}
