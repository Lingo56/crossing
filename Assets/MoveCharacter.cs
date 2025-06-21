using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 moveDirection;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}
