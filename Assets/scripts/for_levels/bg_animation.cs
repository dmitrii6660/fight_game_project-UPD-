using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color color1 = Color.red;
    public Color color2 = Color.blue;

    public float duration = 3.0f; 

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // getting spriterenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not found on obj");
            enabled = false;
        }
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time / duration * 2f * Mathf.PI) + 1f) / 2f;

        spriteRenderer.color = Color.Lerp(color1, color2, t);
    }
}