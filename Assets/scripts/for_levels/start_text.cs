using UnityEngine;
using System.Collections;
using TMPro;

// script for start text
public class start_text : MonoBehaviour
{
    public GameObject temporaryTextObject;
    public float displayDuration = 3f;
    public string initialMessage = "Добро пожаловать в игру!";

    void Start()
    {
        TextMeshProUGUI tmpComponent = temporaryTextObject.GetComponent<TextMeshProUGUI>();

        StartCoroutine(DisplayAndHideCoroutine());
    }

    private IEnumerator DisplayAndHideCoroutine()
    {
        temporaryTextObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        temporaryTextObject.SetActive(false);

        // Destroy(this.gameObject); 
    }
}