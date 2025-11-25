using System.Collections;
using System.Threading;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public RotatePlayerWithHoldPoint playerRotation;
    public SpriteRenderer sr;

    public Sprite normalSprite;
    public Sprite[] executeSprite;

    private float timer = 0f;
    private int currentFrame = 0;
    public float animationSpeed = 0.1f; 
    public SpriteRenderer legs;
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

    private void playerIsExecuting()
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            currentFrame = (currentFrame + 1) % executeSprite.Length;
            sr.sprite = executeSprite[currentFrame];
            timer = 0f;
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
            playerRotation.enabled = false;
            legs.enabled = false;
            playerIsExecuting();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            playerRotation.enabled = true;
            legs.enabled = true;
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
