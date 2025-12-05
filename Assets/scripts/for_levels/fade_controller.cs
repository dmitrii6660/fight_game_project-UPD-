/* tämä on tummenus scripti, jota voi sitten kutsua muissa scripteissä, 
voit kutsua näin: public FadeController fade; <- tähän objecti joka sisältää tän scriptin 
fade.FadeIn() tai fade.FadeOut();*/


using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 1f;

    private Image panelImage;
    private Coroutine currentRoutine;
    private Color originalColor;

    private void Awake()
    {
        panelImage = GetComponent<Image>();

        if (panelImage == null)
        {
            Debug.LogError("panel dont have a Image component");
            return;
        }

        originalColor = panelImage.color;
    }

    public void FadeIn()  => StartFade(originalColor.a);
    public void FadeOut() => StartFade(0f);

    private void StartFade(float targetAlpha)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    private System.Collections.IEnumerator FadeRoutine(float targetAlpha)
    {
        float time = 0f;
        float startAlpha = panelImage.color.a;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            Color c = panelImage.color;
            c.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            panelImage.color = c;

            yield return null;
        }

        // final value
        Color final = panelImage.color;
        final.a = targetAlpha;
        panelImage.color = final;

        currentRoutine = null;
    }
}
