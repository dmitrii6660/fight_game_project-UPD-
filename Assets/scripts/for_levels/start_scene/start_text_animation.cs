using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textElement;
    [TextArea] public string fullText;
    public float delay = 0.05f;
    public float popScale = 1.3f;      
    public float popDuration = 0.1f;  

    void Start()
    {
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        textElement.text = "";
        textElement.ForceMeshUpdate();

        for (int i = 0; i < fullText.Length; i++)
        {
            textElement.text += fullText[i];
            textElement.ForceMeshUpdate();

            StartCoroutine(AnimateLetter(i));
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("text is show!");
    }

    private IEnumerator AnimateLetter(int index)
{
    textElement.ForceMeshUpdate();
    TMP_TextInfo textInfo = textElement.textInfo;

    if (index >= textInfo.characterCount) yield break;
    TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
    if (!charInfo.isVisible) yield break;

    int matIndex = charInfo.materialReferenceIndex;
    int vertIndex = charInfo.vertexIndex;

    // getting start settings
    Vector3[] sourceVerts = textInfo.meshInfo[matIndex].vertices;
    Vector3[] original = new Vector3[4];
    for (int i = 0; i < 4; i++)
        original[i] = sourceVerts[vertIndex + i];

    Vector3 center = (original[0] + original[2]) / 2;

    float time = 0f;

    while (time < popDuration)
    {
        //
        textElement.ForceMeshUpdate();
        textInfo = textElement.textInfo;

        Vector3[] verts = textInfo.meshInfo[matIndex].vertices;
        float t = time / popDuration;
        float scale = Mathf.Lerp(popScale, 1f, t);

        for (int i = 0; i < 4; i++)
            verts[vertIndex + i] = (original[i] - center) * scale + center;

        textElement.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

        time += Time.deltaTime;
        yield return null;
    }

    // back start settings
    textElement.ForceMeshUpdate();
    textInfo = textElement.textInfo;
    Vector3[] finalVerts = textInfo.meshInfo[matIndex].vertices;

    for (int i = 0; i < 4; i++)
        finalVerts[vertIndex + i] = original[i];

    textElement.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
}

}
