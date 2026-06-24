using UnityEngine;

public class ThirdPersonMouseLook : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset = new Vector3(0f, 2f, -4f);
    
    public float lookAtHeightOffset = 1.4f;

    public float mouseSensitivity = 2f;
    public float minPitch = -25f;
    public float maxPitch = 60f;

    public bool enableSmoothing = true;
    public float smoothSpeed = 15f;

    private float pitch = 0f;
    private float yaw = 0f;
    private Vector3 lookAtOffset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = player != null ? player.eulerAngles.y : 0f;
        pitch = transform.eulerAngles.x;

        lookAtOffset = new Vector3(0f, lookAtHeightOffset, 0f);
    }

    void LateUpdate()
    {
        if (player == null) return;

        // --- Input ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        yaw   += mouseX;
        pitch -= mouseY;
        pitch  = Mathf.Clamp(pitch, minPitch, maxPitch);
        player.rotation = Quaternion.Euler(0f, yaw, 0f);
        
        Quaternion camRotation   = Quaternion.Euler(pitch, yaw, 0f);
        Vector3    desiredPosition = player.position + (camRotation * cameraOffset);
        transform.position = enableSmoothing
            ? Vector3.Lerp(transform.position, desiredPosition,
                           1f - Mathf.Exp(-smoothSpeed * Time.deltaTime))  
            : desiredPosition;

        transform.LookAt(player.position + lookAtOffset);
    }
}
