using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 2f;
    public float gravity = -9.81f;
    public Transform playerCamera;  // Reference to the camera for vertical rotation

    private float rotationX = 0;
    private float rotationY = 0;
    private float verticalVelocity = 0;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Gravity
        if (controller.isGrounded)
        {
            verticalVelocity = 0f; // Reset vertical velocity when grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity when not grounded
        }

        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);

        // Mouse look
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed; // Invert Y-axis for FPS look

        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Clamp vertical rotation to avoid flipping

        // Rotate the player body horizontally (left/right)
        transform.localEulerAngles = new Vector3(0, rotationX, 0);

        // Rotate the camera vertically (up/down)
        playerCamera.localEulerAngles = new Vector3(rotationY, 0, 0);
    }
}
