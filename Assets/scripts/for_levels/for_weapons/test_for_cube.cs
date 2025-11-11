using TMPro;
using UnityEngine;
using TMPro;
using System.Collections;

public class test_for_cube : MonoBehaviour
{
    // for bg animation when enemy is destroyed
    public FlickerEffect targetFlickerScript;

    // test texts
    /* public TextMeshProUGUI pts;
    public TextMeshProUGUI pts_hover;
    private int score; */
    
    // test colors
    public Color emptyHandColor = Color.blue;
    public Color itemHandColor = Color.green;

    // other components
    private SpriteRenderer cubeRenderer;
    private PlayerPickup playerScript; // link on player script

    void Start()
    {
        cubeRenderer = GetComponent<SpriteRenderer>();
    }

    // when player entering into trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if player have "Player" tag
        if (other.CompareTag("Player"))
        {
            // getting link on script
            playerScript = other.GetComponent<PlayerPickup>();

            if (playerScript != null && Input.GetMouseButtonDown(1))
            {
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
        }
    }

    // when player is staying in trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
         Debug.Log("player in trigger");
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
        }
    }
}