using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class enemyScript : MonoBehaviour
{
    [Header("Скрипт рандомного движения")]
    public EnemyMovement random; // Сюда нужно перетащить скрипт EnemyMovement в инспекторе

    private Rigidbody2D enemyRB;
    public Sprite giveUpSprite; // можно игнорировать
    public float moveSpeed = 5.0f; //скорсть перемещение

    public Transform enemyHoldPoint; //рука врага
    public SpriteRenderer legs; //можно игнорировать
    public GameObject player; // игрок
    public GameObject playerSprite; // можешь игнорировать

    private bool inTrigger = false; // попал ли игрок по врагу

    private bool isTiming = false; // время пока игрок может добить врага

    public float spotDistance; // как далеко враг может видеть

    private float distance; 

    private bool canSeePlayer; // видет ли враг игрока

    // можно игнорировать
    public Sprite[] movementSprites;
    public Sprite idleSprite; 
    public Sprite damagedSprite; 

    // можно игнорировать
    private int currentFrame = 0;
    private float timer = 0f;
    public float animationSpeed = 0.1f; 
    public SpriteRenderer sr; 

    
    private bool isDamaged = false; // если игрок ударил по врагу
    private bool withWeapon; //есть ли у врага оружие

    private Vector2 direction;

    private Transform targetWeapon;

    private bool enemySeeWeapon = false; // видет ли враг оружие


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
    // логика врага
    private void enemyLogic()
    {
        // Вычисляем направление, только если есть цель (чтобы избежать ошибок)
        if (player != null)
        {
             direction = player.transform.position - transform.position;
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // если у врага есть оружие проверяем видит ли он игрока
        if (withWeapon)
        {
            // дистанциа до игрока
            Vector2 dirToPlayer = player.transform.position - transform.position;
            float distToPlayer = dirToPlayer.magnitude;

            // игнорим эти слои
            int layerMask = ~LayerMask.GetMask("Enemy", "Weapon", "AttackRadius"); 
            
            // raycast to player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer.normalized, spotDistance, layerMask);

            // проверяем куда попал луч
            if (hit.collider != null && hit.collider.gameObject == player)
            {
                canSeePlayer = true;

                // если враг видет игрока на растоянии
                if (distToPlayer < spotDistance)
                {
                    // --- ИЗМЕНЕНИЕ: Враг видит игрока ---
                    // Отключаем рандомное хождение
                    if (random != null) random.enabled = false;
                    
                    // Обнуляем физическую скорость, чтобы MoveTowards работал корректно
                    if (enemyRB != null) enemyRB.velocity = Vector2.zero;

                    MoveToTarget(player.transform.position);
                }
            }
            else
            {
                canSeePlayer = false;
                
                // --- ИЗМЕНЕНИЕ: Враг потерял игрока ---
                // Включаем рандомное хождение
                if (random != null) random.enabled = true;
            }
        }
        else // если у врага нет оружия
        {
            canSeePlayer = false; 

            // ищем оружия на растоянии
            Collider2D[] potentialWeapons = Physics2D.OverlapCircleAll(transform.position, spotDistance);
            
            float minDist = Mathf.Infinity;
            Transform bestTarget = null;

            // враг может брать только одно оружие, ищем ближайшие оружие
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

            // если оружие найдено, проверяем не находится ли оно за стеной
            if (bestTarget != null)
            {
                Vector2 dirToWeapon = bestTarget.position - transform.position;
                int layerMask = ~LayerMask.GetMask("Enemy", "AttackRadius", "Player");
                
                // луч до оружия
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToWeapon.normalized, spotDistance, layerMask);

                // в игре у оружий есть тэг "Pickup", проверяем наличиа тэга на обьекте
                if (hit.collider != null && hit.collider.CompareTag("Pickup"))
                {
                    Debug.Log("enemy see weapon jj");
                    enemySeeWeapon = true;
                                
                    // --- ИЗМЕНЕНИЕ: Враг видит оружие ---
                    // Отключаем рандомное хождение
                    if (random != null) random.enabled = false;
                    
                    if (enemyRB != null) enemyRB.velocity = Vector2.zero;

                    MoveToTarget(bestTarget.position);
                }
                else
                {
                    enemySeeWeapon = false;
                    // --- ИЗМЕНЕНИЕ: Враг не видит оружие (за стеной) ---
                    if (random != null) random.enabled = true;
                }
            }
            else
            {
                enemySeeWeapon = false; // если оружие не в радиусе видимости
                
                // --- ИЗМЕНЕНИЕ: Оружия рядом нет вообще ---
                // Включаем рандомное хождение
                if (random != null) random.enabled = true;
            }
        }
    }

    // enemy is moving to target
    void MoveToTarget(Vector3 targetPos)
    {
        // движение
        transform.position = Vector2.MoveTowards(
            transform.position, 
            targetPos, 
            moveSpeed * Time.deltaTime
        );

        // поворот врага
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
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
        enemyRB = GetComponent<Rigidbody2D>();
        
        // На старте, если скрипт назначен, включаем рандомное движение
        if (random != null) random.enabled = true;
        random.enabled = true;
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

       
        enemyMoveAnimation();
        
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
