using System.Collections;
using UnityEngine;

public class MoveWithPause : MonoBehaviour
{
    public float offset = 1f;       
    public float moveTime = 0.5f;   
    public float waitTime = 0.3f;   

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
           
            yield return MoveTo(startPos.x + offset);
            yield return new WaitForSeconds(waitTime);

           
            yield return MoveTo(startPos.x);
            yield return new WaitForSeconds(waitTime);

            
            yield return MoveTo(startPos.x - offset);
            yield return new WaitForSeconds(waitTime);

           
            yield return MoveTo(startPos.x);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveTo(float targetX)
    {
        float startX = transform.position.x;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;

            float newX = Mathf.Lerp(startX, targetX, t);

            transform.position = new Vector3(newX, startPos.y, startPos.z);

            yield return null;
        }
    }
}
