/* Tämä scripti on tarkoitettu pisteiden lisämis animaatiolle, eli kun pelaaja saa pisteitä
niin ne nopasti muuttu eikä heti*/


using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float speed = 100f; // luvun muuttumis nopeus

    private int currentValue = 0; // nykyinen luku
    private int targetValue = 0;
    private Coroutine animRoutine;

    private void Start()
    {
        UpdateText();
    }

    public void AddScore(int amount)
    {
        targetValue += amount;

        if (animRoutine != null)
            StopCoroutine(animRoutine);

        animRoutine = StartCoroutine(AnimateScore());
    }

    private System.Collections.IEnumerator AnimateScore()
    {
        while (currentValue < targetValue)
        {
            currentValue += Mathf.CeilToInt(speed * Time.deltaTime);

            if (currentValue > targetValue)
                currentValue = targetValue;

            UpdateText();
            yield return null;
        }
    }

    private void UpdateText()
    {
        scoreText.text = "pts " + currentValue.ToString();
    }
}
