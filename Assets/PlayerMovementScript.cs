using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float rotationSpeed = 1f;

    private PlayerInput playerInput;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController component missing!");
        }
    }

    void OnEnable()
    {
        // Subscribe to input events
        if (playerInput != null)
        {
            playerInput.onActionTriggered += OnAction;
        }
    }

    void OnDisable()
    {
        // Unsubscribe when disabled
        if (playerInput != null)
        {
            playerInput.onActionTriggered -= OnAction;
        }
    }

    void OnDestroy()
    {
        // Ensure cleanup of subscriptions
        if (playerInput != null)
        {
            playerInput.onActionTriggered -= OnAction;
        }
    }

    void OnAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                moveInput = context.ReadValue<Vector2>();
                break;
            case "Look":
                lookInput = context.ReadValue<Vector2>();
                break;
            case "Jump":
                if (isGrounded && context.performed)
                {
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }
                break;
        }
    }

    private void Update()
    {
        if (controller == null) return;

        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Convert input into world-space movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * (moveSpeed * Time.deltaTime));

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate player based on mouse X movement
        transform.Rotate(Vector3.up * lookInput.x * rotationSpeed * Time.deltaTime);
    }
}