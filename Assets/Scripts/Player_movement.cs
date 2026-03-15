using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        // W - Forward
        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1f;
        }
        // S - Backward
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f;
        }
        // D - Right
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }
        // A - Left
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        Vector3 move = new Vector3(moveX, 0, moveZ);
        transform.Translate(move * speed * Time.deltaTime);
    }
}