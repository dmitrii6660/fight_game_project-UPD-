using System.Collections;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private bool inTrigger = false;

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

        if(Input.GetMouseButtonDown(0) && inTrigger == true)
        {
            
        }
    }
}
