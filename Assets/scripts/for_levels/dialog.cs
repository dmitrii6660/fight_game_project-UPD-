/* Tämä scripti on tarkoitettu dialogin toimimiseen */

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;
using System.Collections;

public class dialog : MonoBehaviour
{
    // dialog asiat
    public TextMeshProUGUI dialogUI;

    public string[] dialogText;
    private int currentDialog = 0;
    public GameObject dialogObj; 

    // pelaajan asiat

    public GameObject player;
    movement playerMove;

    Rigidbody2D playerRB;

    // objekti joka tulee kun dialogi on mennyt
    public GameObject target;

    // tarvittava bool muuttuja jolla tiedetään onko pelaaja trigerin sisällä vai ei
    private bool inTrigger = false;

    public GameObject startSceneAnimation;

    public Fade_script fade;

    // kun pelaaja astuu dialog trigerin sisälle
    private void OnTriggerEnter2D()
    {
        dialogObj.SetActive(true); // näytetään tektit (dialogit)
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll; // jotta pelaaja ei pystyisi liikkumaan
        playerMove.enabled = false; // jotta pelaaja ei pystyisi liikkumaan
        inTrigger = true; // asennetaan inTrigger = true, eli pelaaja on triggerin sisällä 
    }

    // funktio joka vaihtaa dialogin tekstiä
    private void dialogChange()
    {
        // jos dialogin tekstejä on jäljellä
        if(currentDialog < dialogText.Length)
        {
            currentDialog += 1;
            dialogUI.text = dialogText[currentDialog];
        }
        // jos dialogin tekstit on loppunut
        else
        {
            playerRB.constraints = RigidbodyConstraints2D.None; // pelaaja voi liikkua
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation; // pelaaja voi liikkua mutta ei voi kääntyä
            playerMove.enabled = true; // pelaaja voi liikkua

            // laitetaan teksti pois päältä
            gameObject.SetActive(false); 
            dialogUI.text = ""; 
            dialogObj.SetActive(false); 

            target.SetActive(true); // laitetaan target päälle
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSceneAnimation.SetActive(false);
        // laitetaan fade päälle
        StartCoroutine(fadeCoroutine());
    
        // laitetaan aluksi tekstit pois päältä
        dialogObj.SetActive(false); 
        dialogUI.text = " ";

        // otetaan pelaajan tarvittavat komponentit
        playerRB = player.GetComponent<Rigidbody2D>(); 
        playerMove = player.GetComponent<movement>();

        //laitetaan target objekti pois päältä
        target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // jos pelaaja on triggerin sisällä ja pelaaja on painanut RMB, niin dialogi vaihtuu
        if(Input.GetMouseButtonDown(0) && inTrigger == true) 
        {
            dialogChange();
        }
    }

    // fade coroutine
    private IEnumerator fadeCoroutine()
    {
        fade.GetComponent<Fade_script>().FadeIn(0.5f);
        yield return new WaitForSeconds(1);
        fade.GetComponent<Fade_script>().FadeOut(0.5f);
    }
}
