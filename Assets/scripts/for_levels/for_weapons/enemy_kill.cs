using TMPro;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor;

public class test_for_cube : MonoBehaviour
{
    // for bg animation when enemy is destroyed
    public FlickerEffect targetFlickerScript;

    private TrackableItem trackableItem;
    
    // test colors
    public Color emptyHandColor = Color.blue;
    public Color itemHandColor = Color.green;

    // other components
    private SpriteRenderer cubeRenderer;
    private PlayerPickup playerScript; // link on player scrip

    //test var
    private bool inTrigger;

    void Start()
    {
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
            // little test
            Debug.Log("player has leaved");
            inTrigger = false;
        }
    }

    // interract logic
    private void Interact()
    {
        // checking what is in player holdPoint
        GameObject item = playerScript.GetHeldItem();

        if (item == null)
        {
            cubeRenderer.color = emptyHandColor;
            Debug.Log("hitted by hands");
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
}