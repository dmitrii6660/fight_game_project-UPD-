using UnityEngine;

public class player_animation : MonoBehaviour
{
    public Sprite[] movementSprites; 
    public Sprite idleSprite;      

    public float animationSpeed = 0.1f; 

    private SpriteRenderer sr;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }

    void Update()
    {
        // if player press WASD
        bool isMoving = Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.D);

        if (isMoving && movementSprites.Length > 0)
        {
            // timer for sprite change
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
            // if player dont move
            sr.sprite = idleSprite;
            currentFrame = 0;
            timer = 0f;
        }
    }
}
