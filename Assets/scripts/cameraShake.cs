/* tämä scripti on tarkoitettu cameran shake effectiä varten, kiinitä tämä scripti cameralle
ja sitten voit muissa scripteissä käyttää shake effect functioita*/

using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    /// <summary>
    /// aloitetaan kameran shake effect
    /// </summary>
    /// <param name="duration">kesto</param>
    /// <param name="magnitude">tärinän voimakkuus</param>
    public void Shake(float duration, float magnitude)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            yield return null;
        }

        // palautetaan asento (position)
        transform.localPosition = originalPos;
        shakeRoutine = null;
    }
}
