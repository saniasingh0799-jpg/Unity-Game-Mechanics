using UnityEngine;

public class ThirdPersonMouseLook : MonoBehaviour
{
    [Header("Target Follow")]
    public Transform player;          

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
    public float smoothSpeed = 15f;   

    private float xRotation = 0f;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        xRotation = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (player == null) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (Mathf.Abs(mouseX) > 0.02f)
        {
            player.Rotate(Vector3.up * mouseX);
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);
        Quaternion targetRotation = Quaternion.Euler(xRotation, player.eulerAngles.y, 0f);
        Vector3 desiredPosition = player.position + (targetRotation * cameraOffset);
        
        if (enableSmoothing)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }
        else
        {
            transform.position = desiredPosition;
        }
        Vector3 targetLookPoint = player.position + Vector3.up * lookAtHeightOffset;
        transform.LookAt(targetLookPoint);
    }
}
