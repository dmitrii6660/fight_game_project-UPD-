using TMPro;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class enemy_kill : MonoBehaviour
{
    public GameObject player;
    movement playerMove;

    Rigidbody2D playerRB;

    public GameObject idleSprite;
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

    public SpriteRenderer playerSprite;

    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMove = player.GetComponent<movement>();
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
        if (other.CompareTag("PlayerAttackRadius"))
        {
              //playerSR.sprite = executeSprite[0];
            
            // getting link on script
            playerScript = player.GetComponent<PlayerPickup>();

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
        if (other.CompareTag("PlayerAttackRadius"))
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
            enemyRouting.enemyDamaged = true;
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

    /* when enmy is lies*/
    private IEnumerator enemyHitCoroutine()
    {
        isTiming = true;

        float startTime = Time.time;
        float duration = 3f;

        while (Time.time < startTime + duration) 
        {
            if (inTrigger && Input.GetKeyDown(KeyCode.Space)) 
            {
                playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
                Debug.Log("enemy destroyed");
                player.transform.position = gameObject.transform.position;
                playerSprite.transform.rotation = gameObject.transform.rotation;
                StartCoroutine(executeCoroutine());
                // game logic if enemy is destroy
                break; 
            }
            idleSprite.SetActive(false);
            yield return null; 
        }

        isTiming = false;
        //Debug.Log("timing is false");
        idleSprite.SetActive(true);
        enemyRouting.enemyDamaged = false;
        enemyLogic.enabled = true;
    }

    // when player is executing enemy
    public IEnumerator executeCoroutine()
    {
        routiing.isExecute = true;
        playerMove.enabled = false;   
        yield return new WaitForSeconds(2);
        playerMove.enabled = true;
        DestroyItem();
        routiing.isExecute = false;
        playerRB.constraints = RigidbodyConstraints2D.None;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return null;
    }
}