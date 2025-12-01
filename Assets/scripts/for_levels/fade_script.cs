using UnityEngine;
using System.Collections;

// script for fade in/fade out
public class Fade_script : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        //StartCoroutine(startFadeCoroutine());
        // test
        //GetComponent<Fade_script>().FadeIn(1f);
        //GetComponent<Fade_script>().FadeOut(2f);
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(FadeTo(1f, duration)); // fade in
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeTo(0f, duration)); // fade out
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = sr.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            yield return null;
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, targetAlpha);
    }
}
