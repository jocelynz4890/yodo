using UnityEngine;
using UnityEngine.InputSystem;

public class PaulaPlayerScript : MonoBehaviour
{
    // Modifiable Movement Parameters
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float gravityValue = -9.81f;
    [SerializeField] public float jumpHeight = 1.0f;
    [SerializeField] public float rotationSpeed = 1f;
    //For OnMove and OnLook to Write to
    private Vector2 lookInput;
    private Vector2 moveInput;
    //Initialize Controller, and Variables for movement Calculations
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;


    //Initialize the Rigidbody for reference in Update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    // Called by PlayerInput component when move action occurs
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    private void Update()
    {
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
        transform.Rotate(Vector3.up * lookInput.x * rotationSpeed);
    }
}