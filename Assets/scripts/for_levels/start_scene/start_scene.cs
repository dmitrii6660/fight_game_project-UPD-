/*
tässä scriptissä otetaan muutama asia pois päältä aluksi, eli kun scene alkaa 
piilotetaan pisteitä, pelaaja ja laitetaan pelaajan liikkumis scriptin pois.

kun alku animaatio on mennyt laitetaan kaikki jutut päälle.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class start_scene : MonoBehaviour
{
    public GameObject pts_text; // pts text
    public GameObject startText; // start textes: secene name & scene number
 
    public movement movement; // player movement script

    public GameObject player; // player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set all scripts and texts unactive mode
        pts_text.SetActive(false);
        movement.enabled = false;
        StartCoroutine(startGameCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator startGameCoroutine()
    {
        // start animatio length is 6 sec
        yield return new WaitForSeconds(6f);
        // when start animatio is over, deleting start textes
        Destroy(startText);

        player.transform.position = new Vector3(0, 0, 0);
        movement.enabled = true;
        pts_text.SetActive(true);
    }
    

}
