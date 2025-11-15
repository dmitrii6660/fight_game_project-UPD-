using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textElement;         
    [TextArea]
    public string fullText;            
    public float delay = 0.05f;       

    void Start()
    {
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        textElement.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            textElement.text += fullText[i]; 
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("text is shwo!");
    }
}
