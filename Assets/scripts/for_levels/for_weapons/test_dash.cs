using UnityEngine;

public class DashTowardMouse : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashDistance = 3f;
    public KeyCode dashKey = KeyCode.E;
    public float stopVelocityThreshold = 0.2f; // скорость, ниже которой считаем что объект остановился

    private Rigidbody2D rb;
    private Vector2 dashStartPos;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            StartDash();
        }

        if (isDashing)
        {
            // --- проверка дистанции ---
            float traveled = Vector2.Distance(dashStartPos, rb.position);
            if (traveled >= dashDistance)
            {
                rb.velocity = Vector2.zero;
                isDashing = false;
                Debug.Log("Dash ended (distance limit)");
                return;
            }

            // --- проверка скорости ---
            if (rb.velocity.magnitude < stopVelocityThreshold)
            {
                Debug.Log("object is stopped (low velocity)");
                isDashing = false;
            }
        }
    }

    void StartDash()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mouseWorldPos - rb.position).normalized;

        dashStartPos = rb.position;
        isDashing = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
    }
}
