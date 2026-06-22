using UnityEngine;

public class ThirdPersonMouseLook : MonoBehaviour
{
    [Header("Target Follow")]
    public Transform player;          // Drag your Player GameObject here

    [Header("Camera Position (Offsets)")]
    public Vector3 cameraOffset = new Vector3(0f, 2f, -4f);
    [Tooltip("Adjusts how high up on the player's body the camera points (e.g., 1.5 = head/chest level)")]
    public float lookAtHeightOffset = 1.4f;

    [Header("Mouse Sensitivity & Limits")]
    public float mouseSensitivity = 2f;
    public float minPitch = -25f;
    public float maxPitch = 60f;

    [Header("Camera Smoothing")]
    public bool enableSmoothing = true;
    public float smoothSpeed = 15f;   // Higher numbers = tighter stickiness, lower = smoother follow

    private float xRotation = 0f;

    void Start()
    {
        // 1. Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 2. Initialize the vertical angle based on current camera pitch to prevent a frame-1 snap
        xRotation = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 1. Gather raw mouse axis movements
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 2. REDESIGN FIX: Prevent startup movement/rotation drift
        // Only rotate the player if the mouse is actively moving beyond a microscopic deadzone
        if (Mathf.Abs(mouseX) > 0.02f)
        {
            player.Rotate(Vector3.up * mouseX);
        }

        // 3. Clamp vertical look tilt
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);

        // 4. Calculate the desired target position behind the player
        Quaternion targetRotation = Quaternion.Euler(xRotation, player.eulerAngles.y, 0f);
        Vector3 desiredPosition = player.position + (targetRotation * cameraOffset);

        // 5. REDESIGN ADDITION: Smooth position translation (Feels like a AAA game)
        if (enableSmoothing)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
        else
        {
            transform.position = desiredPosition;
        }

        // 6. Focus point: Look at the player's upper body instead of their feet
        Vector3 targetLookPoint = player.position + Vector3.up * lookAtHeightOffset;
        transform.LookAt(targetLookPoint);
    }
}