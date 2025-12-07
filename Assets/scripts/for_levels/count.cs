using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float speed = 100f; // скорость увеличения числа (чем больше — тем быстрее)

    private int currentValue = 0;
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
