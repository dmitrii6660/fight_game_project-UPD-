using System.Reflection.Emit;
using UnityEngine;

public class menu_Label_animation : MonoBehaviour
{
    public float angle = 10f;      // max rotate value
    public float speed = 2f;       // rotate speed

    void Update()
    {
        float tilt = Mathf.Sin(Time.time * speed) * angle;
        transform.localRotation = Quaternion.Euler(0, 0, tilt);
    }
}
