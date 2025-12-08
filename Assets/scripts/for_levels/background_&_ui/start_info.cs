/* 
tämä scripti on tarkoitettu alku textille joka tulee vain yhden kerran
*/

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeText : MonoBehaviour
{
    private static bool textShownInThisVisit = false;
    public TextMeshProUGUI text;
    public float fadeDuration = 1f;
    public float visibleDuration = 2f;

    void Start()
    {
        if (!textShownInThisVisit)
        {
            ShowText();
            textShownInThisVisit = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator FadeInAndOut()
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;

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
  
    void OnDestroy()
    {
        // flag is false when is leaved from scene (not reloaded)
        if (SceneManager.GetActiveScene().name != "level_2")
        {
            textShownInThisVisit = false;
        }
    }

    void ShowText()
    {
        // showning text
        Debug.Log("text is show!");
        gameObject.SetActive(true);
        StartCoroutine(FadeInAndOut());
    }
}
