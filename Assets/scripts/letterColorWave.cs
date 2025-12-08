using UnityEngine;
using TMPro;

public class LetterColorWave : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Color highlightColor = Color.red;
    public float speed = 0.1f;

    private TMP_TextInfo textInfo;
    private Color32[] originalColors;
    private int currentIndex = 0;

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;

        // Сохраняем оригинальные цвета
        originalColors = new Color32[textInfo.characterCount];
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;
            originalColors[i] = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex]
                .colors32[textInfo.characterInfo[i].vertexIndex];
        }

        StartCoroutine(Animate());
    }

    System.Collections.IEnumerator Animate()
    {
        while (true)
        {
            textMeshPro.ForceMeshUpdate();
            textInfo = textMeshPro.textInfo;

            // Вернуть предыдущую букву
            int prevIndex = (currentIndex - 1 + textInfo.characterCount) % textInfo.characterCount;
            RestoreColor(prevIndex);

            // Выделить текущую букву
            HighlightColor(currentIndex);

            currentIndex = (currentIndex + 1) % textInfo.characterCount;

            yield return new WaitForSeconds(speed);
        }
    }

    void HighlightColor(int index)
    {
        if (!textInfo.characterInfo[index].isVisible) return;

        int meshIndex = textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;

        Color32[] colors = textInfo.meshInfo[meshIndex].colors32;

        colors[vertexIndex] = highlightColor;
        colors[vertexIndex + 1] = highlightColor;
        colors[vertexIndex + 2] = highlightColor;
        colors[vertexIndex + 3] = highlightColor;

        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    void RestoreColor(int index)
    {
        if (!textInfo.characterInfo[index].isVisible) return;

        int meshIndex = textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;

        Color32 original = originalColors[index];
        Color32[] colors = textInfo.meshInfo[meshIndex].colors32;

        colors[vertexIndex] = original;
        colors[vertexIndex + 1] = original;
        colors[vertexIndex + 2] = original;
        colors[vertexIndex + 3] = original;

        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
