using UnityEngine;
using System.Collections;

public class level_animation : MonoBehaviour
{
    public float angle = 10f;     
    public float speed = 2f;       
    public float delay = 0.5f;    

    private bool rotatingRight = true;

    void Start()
    {
        StartCoroutine(RotatePalm());
    }

    private IEnumerator RotatePalm()
    {
        while (true)
        {
            float startAngle = rotatingRight ? -angle : angle;
            float endAngle = rotatingRight ? angle : -angle;
            float t = 0f;

            // smooth rotate
            while (t < 1f)
            {
                t += Time.deltaTime * speed;
                float currentAngle = Mathf.Lerp(startAngle, endAngle, t);
                transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
                yield return null;
            }

            // little stop before start animation again
            yield return new WaitForSeconds(delay);

            rotatingRight = !rotatingRight; // Меняем направление
        }
    }
}
