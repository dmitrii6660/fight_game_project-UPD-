/* Tämä scripti tulee kun taso on hoidettu kokonaan.
scripti näyttää mitä sinun pitäisi tehdä kun hoidat tason, esimerkiksi: etsiä laukun*/

using TMPro;
using UnityEngine;

public class target : MonoBehaviour
{
    public GameObject targetObject;
    public string message; // joku vinkki viesti esim: find a case

    public GameObject targetUI; // ui joka tulee nakyviin

    public TextMeshProUGUI targetText; // teksti objecti joka tulee näkyviin

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetObject.SetActive(false);
        targetUI.SetActive(false); // aluksi laitetaan kaikki jutut pois päältä
    }

    // kun astutaan objectin sisälle
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && level_storage.isLevelCleared == true)
        {
            targetObject.SetActive(true);
            Debug.Log("u must go to car");
            targetText.text = "go into car";
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(level_storage.isLevelCleared) // katsotaan public static metodista onko taso hoidettu
        {
            // laitetaan teksti ja ui elementi päälle
            targetUI.SetActive(true);
            targetText.text = message;
        }
    }
}
