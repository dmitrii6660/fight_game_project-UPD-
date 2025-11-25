/* 
tässä scriptissä tarkistetaan että onko kaikki viholliset kuollut, jos on niin avataan uuden
kerroksen, jos viiminen kerros on hoidettu niin sitten pelaaja voi mennä autoon ja taso on läpi
Myös tässä scriptissä anettaan pisteitä tapetuista vihollisista
*/


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    //when level is cleared
    public GameObject levelCleared;

    // for next floor trigger
    public GameObject floor2;
    public GameObject floor1;

    // for level cleared text
    public TextMeshProUGUI floor_cleared_text;

    //for pts text
    public TextMeshProUGUI pts;
    public TextMeshProUGUI pts_hover;
    private int score;

    // static link for other scripts
    public static CollectableManager Instance { get; private set; }
    public GameObject finalText;

    // list for all enemies on level 
    [SerializeField] private List<GameObject> remainingItems = new List<GameObject>();

    void Awake()
    {
        levelCleared.SetActive(false);
        floor1.SetActive(false);
        floor2.SetActive(false);
        finalText.SetActive(false);
    
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // for adding enemy when scene start
    public void RegisterItem(GameObject item)
    {
        remainingItems.Add(item);
        //Debug.Log($"enemy added: {remainingItems.Count}");
    }

    // before every destroyed enemy
    public void ItemDestroyed(GameObject destroyedItem)
    {
        // deleting enemy from list
        remainingItems.Remove(destroyedItem);

        Debug.Log($"enemy is destroyed: {remainingItems.Count}");

        // for points adding
        if (int.TryParse(pts.text, out int currentScore))
        {
            pts.text = (currentScore + 100).ToString();
            pts_hover.text = (currentScore + 100).ToString();
        }

        score += 100;
        pts.text = "pts " + score;
        pts_hover.text = "pts " + score;

        // if enemy list
        if (remainingItems.Count == 1)
        {
            floor_cleared_text.text = "floor cleared";

            StartCoroutine(finalTextCoroutine());

            floor2.SetActive(true);
        }
        else if (remainingItems.Count == 0)
        {
            floor_cleared_text.text = "level cleared";

            floor2.SetActive(true);
            floor1.SetActive(true);

            levelCleared.SetActive(true);

            StartCoroutine(finalTextCoroutine());
        }
    }

    // method when all enemies is destroyed

    private IEnumerator finalTextCoroutine()
    {
        finalText.SetActive(true);
        yield return new WaitForSeconds(3f);
        finalText.SetActive(false);
    }

    private IEnumerator levelCompletedCoroutine()
    {
        yield return new WaitForSeconds(1);
    }
}