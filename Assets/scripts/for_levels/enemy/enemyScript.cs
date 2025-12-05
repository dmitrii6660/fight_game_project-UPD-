using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class enemyScript : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    public Sprite giveUpSprite;
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

    private Vector2 direction;

    private Transform targetWeapon;

    private bool enemySeeWeapon = false;


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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // is enemy see a player
        if (withWeapon)
        {
            // distance to player
            Vector2 dirToPlayer = player.transform.position - transform.position;
            float distToPlayer = dirToPlayer.magnitude;

            // ignoring these layers
            int layerMask = ~LayerMask.GetMask("Enemy", "Weapon", "AttackRadius"); 
            
            // raycast to player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer.normalized, spotDistance, layerMask);

            // checking which object raycast is hitted
            if (hit.collider != null && hit.collider.gameObject == player)
            {
                canSeePlayer = true;

                // if enemy see player on distance
                if (distToPlayer < spotDistance)
                {
                    MoveToTarget(player.transform.position);
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else // if enemy dont have a weapon
        {
            canSeePlayer = false; // enemy cant run to player without weapon

            // find weapon in spot radius
            Collider2D[] potentialWeapons = Physics2D.OverlapCircleAll(transform.position, spotDistance);
            
            float minDist = Mathf.Infinity;
            Transform bestTarget = null;

            // find nearest weapon
            foreach (Collider2D col in potentialWeapons)
            {
                if (col.CompareTag("Pickup") && col.transform.parent == null)
                {
                    float d = Vector2.Distance(transform.position, col.transform.position);
                    if (d < minDist)
                    {
                        minDist = d;
                        bestTarget = col.transform;
                    }
                }
            }
            
            targetWeapon = bestTarget;

            // if weapon is findend checking the wall 
            if (bestTarget != null)
            {
                Vector2 dirToWeapon = bestTarget.position - transform.position;
                int layerMask = ~LayerMask.GetMask("Enemy", "AttackRadius", "Player");
                
                // raycast to weapon
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToWeapon.normalized, spotDistance, layerMask);

                // checking is raycast hitted to Pickup
                if (hit.collider != null && hit.collider.CompareTag("Pickup"))
                {
                    enemySeeWeapon = true;
                    //Debug.Log(bestTarget);
                    
                    MoveToTarget(bestTarget.position);
                }
                else
                {
                    enemySeeWeapon = false;
                    Debug.Log("weapon is behind the wall");
                }
            }
            else
            {
                enemySeeWeapon = false; // if weapon is not in spot radius
            }
        }

    // enemy is moving to target
    void MoveToTarget(Vector3 targetPos)
    {
        // moving
        transform.position = Vector2.MoveTowards(
            transform.position, 
            targetPos, 
            moveSpeed * Time.deltaTime
        );

        // rotation
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
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
        else if(other.CompareTag("Pickup") && withWeapon == false)
        {
            Debug.Log("enemy get weapon");
            other.transform.SetParent(enemyHoldPoint);
            withWeapon = true;
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
        foreach (Transform child in enemyHoldPoint)
        {
            child.SetParent(null);
        }
        withWeapon = false;

        if(playerMode.playerHaveWeapon == false)
        {
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
    legs.enabled = false;
    isDamaged = true;
    isTiming = true;
    sr.sprite = damagedSprite;

    float startTime = Time.time;
    float duration = 3f;

    bool spaceSuccess = false; // [FLAG] onko pelaaja ehtinyt painaa SPACE

    while (Time.time < startTime + duration)
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            spaceSuccess = true;

            // käännetään pelaaja vihollista päin
            player.transform.position = gameObject.transform.position;
            playerSprite.transform.rotation =
            Quaternion.Euler(0f, 0f, gameObject.transform.eulerAngles.z + 180f);


            StartCoroutine(executeCoroutine());

            break; // poistutaan while loopista
        }

        yield return null;
    }

    // jos pelaaja on painannut space tämä coroutine ei enää jatku
    if (spaceSuccess)
    {
        yield break; // postutaan coroutinesta
    }

    // jos pelaaja ei ole ehtinyt painaa space
    legs.enabled = true;
    isDamaged = false;
    isTiming = false;
    sr.sprite = idleSprite;
}

    // kun pelaaja on teloittamassa vihollista
    private IEnumerator executeCoroutine()
    {
        legs.enabled = false;
        sr.sprite = damagedSprite;
        isDamaged = true;
        playerMode.playerIsExecuting = true;
        yield return new WaitForSeconds(2);
        destroyEnemy();
        playerMode.playerIsExecuting = false;
    }

    // destroying enemy
    public void destroyEnemy()
    {
        foreach (Transform child in enemyHoldPoint)
        {
            child.SetParent(null);
        }
        // calling to enemy_managr thats obj(this enemy) is destroyed
        CollectableManager.Instance.ItemDestroyed(this.gameObject);

        // destroying current obj
        Destroy(this.gameObject);
        playerMode.playerIsExecuting = false;
    }
   
    void Start()
    {
        CollectableManager.Instance.RegisterItem(this.gameObject); //add enemy to collectable manager

        //asetetaan alku sprite viholisille
        sr.sprite = idleSprite;

        //tarkistetaan onko viholisella ase
    }

    void Update()
    {
        if(enemyHoldPoint.childCount > 0)
        {
            withWeapon = true;
        }
        else
        {
            withWeapon = false;
        }
        if(isDamaged == false)
        {
            enemyLogic();
        }

        if(canSeePlayer == true || enemySeeWeapon == true)
        {
            enemyMoveAnimation();
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
