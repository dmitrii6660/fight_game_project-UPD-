using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    // --- Настраиваемые параметры ---
    [Header("Настройки движения")]
    public float moveSpeed = 3f;
    public float maxMoveTime = 2f;
    public float minMoveTime = 0.5f;

    [Header("Настройки избегания")]
    [Tooltip("Дистанция, на которую бросается луч для проверки препятствий.")]
    public float wallCheckDistance = 0.5f;
    [Tooltip("Слой(и) объектов, которые нужно проверять на наличие Tilemap Collider 2D (обычно это слой тайлмапа).")]
    public LayerMask collisionCheckLayer; 
    
    // --- Внутренние переменные ---
    private Vector2 currentDirection;
    private float moveTimeRemaining;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetNewRandomDirection();
    }

    void Update()
    {
        moveTimeRemaining -= Time.deltaTime;

        // Если время вышло ИЛИ мы обнаружили Tilemap Collider, выбираем новое направление
        if (moveTimeRemaining <= 0f || IsTilemapAhead())
        {
            SetNewRandomDirection();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = currentDirection * moveSpeed;
    }

    void SetNewRandomDirection()
    {
        moveTimeRemaining = Random.Range(minMoveTime, maxMoveTime);
        
        // Выбираем случайное направление по осям
        int randomMove = Random.Range(0, 4); 
        switch (randomMove)
        {
            case 0: currentDirection = Vector2.right; break;
            case 1: currentDirection = Vector2.left; break;
            case 2: currentDirection = Vector2.up; break;
            case 3: currentDirection = Vector2.down; break;
        }

        // --- НОВОЕ: Поворот врага в сторону движения ---
        RotateToDirection();
    }

    /// <summary>
    /// Поворачивает объект в сторону currentDirection
    /// </summary>
    void RotateToDirection()
    {
        // Проверяем, что вектор не нулевой, чтобы избежать ошибок
        if (currentDirection != Vector2.zero)
        {
            // Вычисляем угол в градусах
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
            
            // Применяем поворот вокруг оси Z
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    /// <summary>
    /// Проверяет, находится ли впереди объект с TilemapCollider2D.
    /// </summary>
    /// <returns>True, если обнаружен TilemapCollider2D, иначе False.</returns>
    bool IsTilemapAhead()
    {
        Vector2 rayStartPoint = rb.position;
        
        // Бросаем луч, используя collisionCheckLayer
        RaycastHit2D hit = Physics2D.Raycast(rayStartPoint, currentDirection, wallCheckDistance, collisionCheckLayer);

        // Debug-рисование луча в редакторе
        Debug.DrawRay(rayStartPoint, currentDirection * wallCheckDistance, hit ? Color.red : Color.green);

        // Проверяем, было ли попадание луча
        if (hit.collider != null)
        {
            // Пытаемся получить компонент TilemapCollider2D из объекта, в который попали.
            TilemapCollider2D tilemapCollider = hit.collider.GetComponent<TilemapCollider2D>();

            // Если компонент TilemapCollider2D НАЙДЕН -> поворачиваем
            if (tilemapCollider != null)
            {
                return true; 
            }
        }
        
        // Попадания не было
        return false;
    }
}