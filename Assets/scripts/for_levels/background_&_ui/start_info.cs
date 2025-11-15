/* 
tämä scripti on tarkoitettu alku textille joka tulee animation jälkeen
*/

using UnityEngine;
using TMPro;
using System.Collections;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeDuration = 1f;
    public float visibleDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;

        // starting after start animation
        yield return new WaitForSeconds(8f);
        // smooth fade in
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            text.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        // smooth fade out
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            text.color = color;
            yield return null;
        }
    }
}
