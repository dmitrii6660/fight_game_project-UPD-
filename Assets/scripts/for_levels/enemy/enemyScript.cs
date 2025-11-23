using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class enemyScript : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Transform enemyHoldPoint;
    public GameObject player;

    private bool inTrigger = false;

    private bool isTiming = false;

    public float spotDistance;

    private float distance;


    //is enemy see player or not
    private bool canSeePlayer;

    //for animation
    public Sprite[] movementSprites; 
    public Sprite idleSprite;    
    public Sprite damagedSprite; 
    private int currentFrame = 0;
    private float timer = 0f;
    public float animationSpeed = 0.1f; 
    public SpriteRenderer sr;

    private bool isDamaged = false;
    public bool withWeapon;
    //enemy start settings
    public bool withWeaponStart;
    Vector3 startPosition;

    // 
    private void enemyLogic()
    {
        if(enemyHoldPoint == null)
        {
            withWeapon = true;
        }
        else
        {
            withWeapon = false;
        }

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // is enemy see a player
        int layerMask = ~LayerMask.GetMask("Enemy", "Weapon", "AttackRadius"); // ignoring enemy and weapon layer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, spotDistance, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == player)
            {
                canSeePlayer = true; 
            }
            else
            {
                canSeePlayer = false;
            }
        }

        // is player is in range and see
        if (distance < spotDistance && canSeePlayer)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && withWeapon == true)
        {
            playerMode.playerIsDead = true;
        }
        else if(other.CompareTag("PlayerAttackRadius"))
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerAttackRadius"))
        {
            inTrigger = false;
        }
    }

    private void Interact()
    {
        if(playerMode.playerHaveWeapon)
        {
            enemyHoldPoint.SetParent(null);
            enemyIsHitted();
        }
        else
        {
            destroyEnemy();
        }
    }

    private void enemyIsHitted()
    {
        if (!isTiming)
        {
            StartCoroutine(enemyIsHittedCoroutine());
        }
    }

    //coroutines
    // when enemy is hitted by hands
    private IEnumerator enemyIsHittedCoroutine()
    {
        isDamaged = true;
        isTiming = true;

        float startTime = Time.time;
        float duration = 3f;

        while (Time.time < startTime + duration)
        {
            if (inTrigger && Input.GetKeyDown(KeyCode.Space))
            {
                player.transform.position = gameObject.transform.position;
                StartCoroutine(executeCoroutine());
                // game logic if enemy is destroy
                break; 
                yield return null; 
            }
            isDamaged = false;
            isTiming = false;
        }
    }

    // when player is executing 
    private IEnumerator executeCoroutine()
    {
        playerMode.playerIsExecuting = true;
        yield return new WaitForSeconds(2);
        destroyEnemy();
        playerMode.playerIsExecuting = false;
    }

    // destroying enemy
    public void destroyEnemy()
    {
        // calling to enemy_managr thats obj(this enemy) is destroyed
        CollectableManager.Instance.ItemDestroyed(this.gameObject);

        // destroying current obj
        Destroy(this.gameObject);
    }
   
    void Start()
    {
        startPosition = transform.position;
        // otataan spriteRenderer komponentti
        sr = GetComponent<SpriteRenderer>();
        //asetetaan alku sprite viholisille
        sr.sprite = idleSprite;

        if(enemyHoldPoint != null)
        {
            withWeapon = true;
        }
        else
        {
            withWeapon = false;
        }
    }

    void Update()
    {
        if(isDamaged == false)
        {
            enemyLogic();
        }

        if(playerMode.playerIsDead == true)
        {
            gameObject.transform.position = startPosition;
        }
    }
}
