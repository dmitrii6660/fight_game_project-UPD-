using UnityEngine;
using TMPro;

public class dead : MonoBehaviour
{
    public GameObject deathText;
    public GameObject player;
    movement playerMove;

    private void playerIsDead()
    {
        //Debug.Log("player dead void is started");
        playerMove.enabled = false;
        deathText.SetActive(true);
        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            routiing.isDead = false;
            deathText.SetActive(false);
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
        if(routiing.isDead == true)
        {
            playerIsDead();
        }
        else
        {
            playerMove.enabled = true;
        }
    }
}
