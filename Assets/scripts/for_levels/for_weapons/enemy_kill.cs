using TMPro;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor;

public class test_for_cube : MonoBehaviour
{
    public GameObject idleSprite;
    public SpriteRenderer sr;
    public Sprite damagetSprite;
    public enemyAI enemyLogic;
    // for bg animation when enemy is destroyed
    public FlickerEffect targetFlickerScript;

    private TrackableItem trackableItem;
    
    // test colors
    public Color emptyHandColor = Color.blue;
    public Color itemHandColor = Color.green;

    // other components
    private SpriteRenderer cubeRenderer;
    private PlayerPickup playerScript; // link on player scrip

    private bool isTiming = false;

    private bool inTrigger;

    public Transform holdPoint;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cubeRenderer = GetComponent<SpriteRenderer>();
          // when game starts adding to enemy_list
        CollectableManager.Instance.RegisterItem(this.gameObject);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && inTrigger == true)
        {
            Interact();
        }
    }

    // when player entering into trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if player have "Player" tag
        if (other.CompareTag("Player"))
        {
            // getting link on script
            playerScript = other.GetComponent<PlayerPickup>();

            inTrigger = true;

            if (playerScript != null && Input.GetMouseButtonDown(0))
            {
                Debug.Log("u hitted to obj");
                Interact();
            }
        }
        else
        {
            Debug.Log("is not player");
        }
    }

    // player leave from trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    public void enemyHit()
    {
        if (!isTiming)
        {
            StartCoroutine(enemyHitCoroutine());
        }
    }

    // interract logic
    private void Interact()
    {
        // checking what is in player holdPoint
        GameObject item = playerScript.GetHeldItem();

        if (item == null)
        {
            //cubeRenderer.color = emptyHandColor;
            cubeRenderer.sprite = null;
            cubeRenderer.sprite = damagetSprite;
            enemyLogic.enabled = false;
            holdPoint.SetParent(null);
            enemyHit();
        }
        else
        {
            cubeRenderer.color = itemHandColor;
            Debug.Log("hitted by item");

            targetFlickerScript.StartFlicker();
            DestroyItem();
        }
    }
     public void DestroyItem()
    {
        // calling to enemy_managr thats obj(this enemy) is destroyed
        CollectableManager.Instance.ItemDestroyed(this.gameObject);

        // destroying current obj
        Destroy(this.gameObject);
    }

    private IEnumerator enemyHitCoroutine()
    {
        isTiming = true;

        float startTime = Time.time;
        float duration = 3f;

        while (Time.time < startTime + duration) 
        {
            if (inTrigger && Input.GetKeyDown(KeyCode.Space)) 
            {
                Debug.Log("enemy destroyed");
                // game logic if enemy is destroy
                DestroyItem();
                break; 
            }
            idleSprite.SetActive(false);
            yield return null; 
        }

        isTiming = false;
        Debug.Log("timing is false");
        idleSprite.SetActive(true);
        enemyLogic.enabled = true;
        //cubeRenderer.color = itemHandColor;
    }
}