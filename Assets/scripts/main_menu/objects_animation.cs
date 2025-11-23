using UnityEngine;

public class house_moving : MonoBehaviour
{
    Vector3 currentPos;
    public Transform startPoint;   // start point
    public Transform targetPoint;  // target
    public float speed = 2f;       // moving speed

    void Start()
    {
        // start position for object = current position (only once)
        currentPos = transform.position;
    }

    void Update()
    {
        // moving object to target
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetPoint.position.x, targetPoint.position.y, transform.position.z),
            speed * Time.deltaTime
        );

        // when object is finished to target = teleposrting to start point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            transform.position = startPoint.position;
        }
    }
}
