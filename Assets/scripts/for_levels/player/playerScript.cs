using System.Collections;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb;
    private bool inTrigger = false;

    public Transform holdPoint;

    //player movement script
    movement playerMove;

    //for dead
    public GameObject deathText;
    private void playerIsDead()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        playerMove.enabled = false;
        deathText.SetActive(true);
        if(Input.GetKeyDown(KeyCode.R))
        {
            playerMode.playerIsDead = false; 
            deathText.SetActive(false);
            playerMove.enabled = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMode.playerIsDead == true)
        {
            Debug.Log("player is death!!!!");
            playerIsDead();
        }

        if(playerMode.playerIsExecuting)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        /*if(holdPoint == null)
        {
            playerMode.playerHaveWeapon = false;
        }
        else
        {
            playerMode.playerHaveWeapon = true;
        }*/
    }
}
