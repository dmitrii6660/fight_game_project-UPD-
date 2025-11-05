using UnityEngine;
using TMPro; // Убери, если используешь обычный UI Text

public class pause_text_animation : MonoBehaviour
{
    [Header("Настройки цвета")]
    public Color startColor = Color.cyan;    
    public Color endColor = Color.magenta;   
    public float duration = 2f;             

    [Header("Ссылка на текст")]
    public TextMeshProUGUI textTMP;          

    private float t = 0f;
    private bool reverse = false;

    void Update()
    {
        if (textTMP == null) return; // if text null

     
        t += Time.deltaTime / duration;

      
        Color newColor = Color.Lerp(startColor, endColor, reverse ? 1 - t : t);

       
        textTMP.color = newColor;
   

        // when animtion end = reverse it
        if (t >= 1f)
        {
            t = 0f;
            reverse = !reverse;
        }
    }
}
