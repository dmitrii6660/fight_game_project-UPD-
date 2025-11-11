using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
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
        Debug.Log($"enemy added: {remainingItems.Count}");
    }

    // befory every destroyed enemy
    public void ItemDestroyed(GameObject destroyedItem)
    {
        // deleting enemy from list
        remainingItems.Remove(destroyedItem);

        Debug.Log($"enemy is destroyed: {remainingItems.Count}");
          if (int.TryParse(pts.text, out int currentScore))
            {
                pts.text = (currentScore + 100).ToString();
                pts_hover.text = (currentScore + 100).ToString();
            }

            score += 100;
            pts.text = "pts " + score;
            pts_hover.text = "pts " + score;

        // if enemy list is null (all enemies is destroyed)
        if (remainingItems.Count == 0)
        {
            AllItemsCollected();
        }
    }

    // method when all enemies is destroyed
    private void AllItemsCollected()
    {
        Debug.Log("level clear!");
        //finalText.SetActive(true);
        StartCoroutine(finalTextCoroutine());
    }

    private IEnumerator finalTextCoroutine()
    {
        finalText.SetActive(true);
        yield return new WaitForSeconds(3f);
        finalText.SetActive(false);
    }
}