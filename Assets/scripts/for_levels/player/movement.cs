using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("speed settings")]
    public float moveSpeed = 5f;

    [Header("components")]
    public Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        //getting Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed; 
    }
}
