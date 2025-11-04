using UnityEngine;
using TMPro;

public class TextLoopAnimation : MonoBehaviour
{
    public TextMeshProUGUI text; 
    public float moveDistance = 50f;  
    public float moveSpeed = 2f;     
    public float verticalOffset = 10f; 

    private Vector3 startPos;
    private bool movingLeft = true;

    void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        startPos = text.rectTransform.anchoredPosition;
        StartCoroutine(AnimateText());
    }

    private System.Collections.IEnumerator AnimateText()
    {
        while (true)
        {
            Vector3 targetPos;

            if (movingLeft)
                targetPos = startPos + new Vector3(-moveDistance, -verticalOffset, 0);
            else
                targetPos = startPos;

            float t = 0;
            Vector3 initialPos = text.rectTransform.anchoredPosition;

            while (t < 1)
            {
                t += Time.deltaTime * moveSpeed;
                text.rectTransform.anchoredPosition = Vector3.Lerp(initialPos, targetPos, t);
                yield return null;
            }

            movingLeft = !movingLeft; // change target
        }
    }
}
