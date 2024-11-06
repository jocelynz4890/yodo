using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;  // Controls the speed of the camera rotation
    public Transform playerBody;           // Reference to the player’s body (usually used to rotate the entire body horizontally)

    private float xRotation = 0f;          // Tracks the camera's vertical rotation

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse movement on the X and Y axes
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the xRotation based on mouseY and clamp it between -90 and 90 degrees to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the vertical rotation (x-axis) to the camera itself
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body horizontally (y-axis) based on mouseX
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
