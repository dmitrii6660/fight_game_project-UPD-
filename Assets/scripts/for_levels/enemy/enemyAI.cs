using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float spotDistance;

    private float distance;

    bool canSeePlayer = false;

    // for animation
    public Sprite[] movementSprites; 
    public Sprite idleSprite;    
    public Sprite damagetSprite; 
    private int currentFrame = 0;
    private float timer = 0f;
    public float animationSpeed = 0.1f; 
    public SpriteRenderer sr;

    void enemyAnimation()
    {
        if(canSeePlayer == true)
        {
                sr.sprite = null;
              timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0f;
                currentFrame++;
                if (currentFrame >= movementSprites.Length)
                    currentFrame = 0;
                sr.sprite = movementSprites[currentFrame];
            }
        }
        else 
        {
            // if enemy dont move
            sr.sprite = idleSprite;
            currentFrame = 0;
            timer = 0f;
        }
        }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // is enemy see a player
        int layerMask = ~LayerMask.GetMask("Enemy", "Weapon"); // ignoring enemy and weapon layer
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
                speed * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        enemyAnimation();
    }

    // for visual (style for raycast)
    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}
