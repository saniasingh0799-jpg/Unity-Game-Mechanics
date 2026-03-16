using UnityEngine;

public class Player_jump : MonoBehaviour
{
    private float jumpforce = 10f;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpforce, rb.linearVelocity.z);
        }
    }
}
