using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class enemyScript : MonoBehaviour
{
    public float moveSpeed = 5.0f; //viholisen liikkumis nopeus

    public Transform enemyHoldPoint; //vihollisen holdPoint, eli objekti jolla hän pitää asen
    public SpriteRenderer legs; //vihollisen jalat, tarvitaan animatiolle kun vihollinen liikku
    public GameObject player; // pelaaja
    public GameObject playerSprite;

    private bool inTrigger = false; // onko pelaajan AttackRadius osunut viholliseen

    private bool isTiming = false;

    public float spotDistance; // kuinka pitkälle vihollinen voi nähdä pelaajan

    private float distance; 

    private bool canSeePlayer; // näkeekö vihollinen pelaajan

    // animaatiolle (sprites)
    public Sprite[] movementSprites; //vihollisen liikumis sprites
    public Sprite idleSprite; //vihollisen tavallinen sprite, esim kun vihollinen on paikalla
    public Sprite damagedSprite; // kun vihollista on lyöty

    // animaatiolle
    private int currentFrame = 0;
    private float timer = 0f;
    public float animationSpeed = 0.1f; 
    public SpriteRenderer sr; 

    // muut tarkistuksen liittyviä juttuja
    private bool isDamaged = false; //onko vihollinen nyt maassa
    private bool withWeapon; //onko viholisella ase

    //vihollisen alku asetukset
    private bool withWeaponStart;
    public GameObject enemyStartWeapon; // vihollisen alku ase (haluttaessa)
    Vector3 startPosition;


    // liikumis animaatio funktio
    private void enemyMoveAnimation()
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            currentFrame = (currentFrame + 1) % movementSprites.Length;
            legs.sprite = movementSprites[currentFrame];
            timer = 0f;
        }
    }

    // 
    private void enemyLogic()
    {
        if(enemyHoldPoint.childCount > 0)
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
        if(other.CompareTag("Player") && withWeapon == true && isDamaged == false)
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
        if(playerMode.playerHaveWeapon == false)
        {
            Debug.Log("player has weapon");
            foreach (Transform child in enemyHoldPoint)
            {
                child.SetParent(null);
            }

            enemyIsHitted();
        }
        else
        {
            foreach (Transform child in enemyHoldPoint)
            {
                child.SetParent(null);
            }
            Debug.Log("player no weapon");
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
        legs.enabled = false;
        sr.sprite = damagedSprite;

        float startTime = Time.time;
        float duration = 3f;

        while (Time.time < startTime + duration)
        {
            if (inTrigger && Input.GetKeyDown(KeyCode.Space))
            {
                player.transform.position = gameObject.transform.position;
                playerSprite.transform.rotation = gameObject.transform.rotation;
                StartCoroutine(executeCoroutine());
                // game logic if enemy is destroy
                break; 
            }
            yield return null;
        }
        isDamaged = false;
        isTiming = false;
        legs.enabled = true;
        sr.sprite = idleSprite;
    }

    // when player is executing 
    private IEnumerator executeCoroutine()
    {
        isDamaged = true;
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
        playerMode.playerIsExecuting = false;
    }
   
    void Start()
    {
        //tarkistetaan että vihollisella on ase
        if(enemyHoldPoint.childCount > 0)
        {
            withWeaponStart = true;
            Debug.Log("enemy have a start weapon");
        }
        
        CollectableManager.Instance.RegisterItem(this.gameObject); //add enemy to collectable manager

        startPosition = transform.position; // getting enemy start position

        //asetetaan alku sprite viholisille
        sr.sprite = idleSprite;

        //tarkistetaan onko viholisella ase
    }

    void Update()
    {
        if(isDamaged == false)
        {
            enemyLogic();
        }

        if(canSeePlayer == true)
        {
            enemyMoveAnimation();
        }

        if(playerMode.playerIsDead == true)
        {
            this.gameObject.SetActive(true);
            gameObject.transform.position = startPosition;
            enemyStartWeapon.transform.SetParent(enemyHoldPoint.transform);
        }
        if(Input.GetMouseButtonDown(0) && inTrigger == true)
        {
            Interact();
        }
        if(playerMode.playerIsExecuting)
        {
            legs.enabled = false;
        }
    }
}
