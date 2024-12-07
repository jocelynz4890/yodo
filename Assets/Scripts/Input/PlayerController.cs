using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool fire = false;
    public bool isInteract = false;
    // variables for other scripts to control whether players can do certain things
    public bool canMove = true;
    public bool canLook = true;
    public bool canFire = true;
    public bool canInteract = true;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float lookSpeedY = 0.1f; // Add a look speed for vertical movement

    private PlayerInput playerInput;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private CharacterController controller;
    private Vector3 playerVelocity;

    private Camera playerCamera; // Reference to the player's camera
    private float xRotation = 0f; // To keep track of camera's rotation on the X-axis

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        // Ensure there is a camera attached
        playerCamera = GetComponentInChildren<Camera>(); 

        if (controller == null)
        {
            Debug.LogError("CharacterController component missing!");
        }

        if (playerCamera == null)
        {
            Debug.LogError("Camera component missing!");
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
                moveInput = canMove ? context.ReadValue<Vector2>() : Vector2.zero;
                break;
            case "Look":
                lookInput = canLook ? context.ReadValue<Vector2>() : Vector2.zero;
                break;
            case "Jump":
                break;
            case "Fire":
                fire = canFire ? context.performed : false;
                break;
            case "Interact":
                isInteract = canInteract ? context.performed : false;
                break;
            default:
                break;
        }
    }

    public void ResetInputs()
    {
        moveInput = Vector2.zero;
        lookInput = Vector2.zero;
        fire = false;
        isInteract = false;
    }

    private void Update()
    {
        if (controller == null || playerCamera == null) return;

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Convert input into world-space movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * (moveSpeed * Time.deltaTime));

        // Rotate player based on mouse X movement
        // * Time.deltaTime
        transform.Rotate(Vector3.up * lookInput.x * rotationSpeed);

        // Apply vertical look (camera pitch) based on mouse Y movement
        xRotation -= lookInput.y * lookSpeedY; // Adjust this value for desired sensitivity
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent camera flipping
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate the camera around the X-axis
    }
}
