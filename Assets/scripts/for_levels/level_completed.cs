/* Tässä scriptissä näytetään viimeinen menu kun pelaaja on läppässyt tason, menussa voi valita
siitykö pelaaja seuaavan tason tai level menun*/

using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level_completed : MonoBehaviour
{
    public Color hoverColor;
    public string teleportScene; // taso jolle pelaaja siirty seuraavaksi
    public GameObject targetUI; // ui joka näytetään viimeisenä
    public TextMeshProUGUI[] options; // valinnat
    private int currentIndex = 1; 
    public GameObject endOfLevel;
    movement playerMove; // pelaajan liikkumis scripti

    private Rigidbody2D rb;
    public GameObject player; // pelaaja

    public FadeController fade; // tummennus scripti
    private Color currentColor; // nykyinen väri


    //tässä vaijdetaan tekstin väriä kun pelaaja painaa down arrow tai up arrow
    private void optionSelectorHover()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && currentIndex != 0)
        {
            options[currentIndex].color = currentColor;
            currentIndex -= 1;
            options[currentIndex].color = hoverColor;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && currentIndex != 1)
        {
            options[currentIndex].color = currentColor;
            currentIndex += 1;
            options[currentIndex].color = hoverColor;
        }
    }

    // tässä tarkistetaan mitä pelaaja pn valinnut
    private void optionSelector()
    {
        if(currentIndex == 1 && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(teleportingCoroutine());
            Debug.Log("ure loaded level 2");
        }
        else if(currentIndex == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            //StartCoroutine(teleportingCoroutineMain());
            fade.FadeIn();
            StartCoroutine(teleportingCoroutineMain());
            Debug.Log("ure loaded level menu");
        }
    }

    // kun pelaaja on astunut objektin sisälle
    private void OnTriggerEnter2D()
    {
        targetUI.SetActive(false);
        StartCoroutine(transitionCoroutine());
    }

    // vähittelleen näytetään viimeinen menu
    private IEnumerator transitionCoroutine()
    {
        playerMove.enabled = false; // pelaaja ei voi liikkua
        fade.FadeIn(); // tumennus
        yield return new WaitForSeconds(1);
        endOfLevel.SetActive(true); // näytetään viimeinen menu
        player.transform.position = new Vector3(100, 0, 0);
        yield return new WaitForSeconds(1);
        fade.FadeOut();
    }

    private IEnumerator teleportingCoroutine()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(teleportScene);
    }

     private IEnumerator teleportingCoroutineMain()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("level_menu");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentColor = options[0].color; //otetaan nykyinen väri
        playerMove = player.GetComponent<movement>(); // otetaan pelaajan liikkumis scripti
        Debug.Log("current color " + currentColor);
    }

    // Update is called once per frame
    // kaikki funktiot toimisi aina
    void Update()
    {
        optionSelectorHover();
        optionSelector();
    }
}
