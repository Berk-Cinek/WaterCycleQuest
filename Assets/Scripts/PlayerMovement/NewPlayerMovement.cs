using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float dashTimer;
    private float dashCooldownTimer;
    private bool isDashing;

    public float moveSpeed = 5f;
    private float dashDistance = 10f;

    public Rigidbody2D rigidbody2D;
    public Animator animator;
    private GameInput gameInput;

    private Vector2 lastMoveDir;
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
        
        if (dialogueUI.IsOpen)
        {
            rigidbody2D.velocity = Vector2.zero; 
            movement = Vector2.zero; 
            UpdateAnimator(Vector2.zero); 
            return;
        }

        HandleMovement();

        
        if (Input.GetKeyDown(KeyCode.E) && !dialogueUI.IsOpen)
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing && !dialogueUI.IsOpen)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed * Time.fixedDeltaTime);
            rigidbody2D.velocity = new Vector2(movement.x, movement.y).normalized * _speed;
        }
    }

    private void HandleMovement()
    {
        if (dialogueUI.IsOpen) return; 

        movement = gameInput.Player.Move.ReadValue<Vector2>();
        UpdateAnimator(movement);

        if (movement != Vector2.zero)
        {
            lastMoveDir = movement.normalized;
        }
    }

    private void UpdateAnimator(Vector2 currentMovement)
    {
        animator.SetFloat("Horizontal", currentMovement.x);
        animator.SetFloat("Vertical", currentMovement.y);
        animator.SetFloat("Speed", currentMovement.sqrMagnitude);
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (dashCooldownTimer <= 0f && !isDashing && !dialogueUI.IsOpen)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
        else
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void StartDash()
    {
        if (dialogueUI.IsOpen) return; 

        Debug.Log("dashing");
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        rigidbody2D.velocity = lastMoveDir * dashSpeed;
    }

    private void EndDash()
    {
        isDashing = false;
        rigidbody2D.velocity = Vector2.zero;
    }
}
