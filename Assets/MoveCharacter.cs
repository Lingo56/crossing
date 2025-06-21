using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 moveDirection;

    void Start()
    {
        // Optionally, get the Rigidbody2D component if not assigned in Inspector
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        //moveDirection = new Vector2(-1.0f, -1.0f).normalized;
        Debug.Log("Move Direction: " + moveDirection);
    }

    void FixedUpdate()
    {
        // Move the character based on input
        rb.linearVelocity = moveDirection * speed;
    }
}
