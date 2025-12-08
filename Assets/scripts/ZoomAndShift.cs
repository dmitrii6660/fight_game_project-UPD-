using UnityEngine;

public class OneDirectionZoomShift : MonoBehaviour
{
    [Header("Настройки увеличения")]
    public float maxScale = 1.4f;       // максимальный размер
    public float zoomSpeed = 2f;        // скорость увеличения

    [Header("Настройки смещения")]
    public float shiftDistance = 1f;    // насколько сдвигается
    public bool moveRight = true;       // TRUE = вправо, FALSE = влево
    public float moveSpeed = 2f;        // скорость смещения

    private Vector3 startScale;
    private Vector3 startPos;

    private Vector3 targetScale;
    private Vector3 targetPos;

    void Start()
    {
        startScale = transform.localScale;
        startPos = transform.localPosition;

        // целевой масштаб
        targetScale = startScale * maxScale;

        // смещение в одну сторону
        float direction = moveRight ? 1 : -1;
        targetPos = startPos + new Vector3(shiftDistance * direction, 0, 0);
    }

    void Update()
    {
        // плавно увеличиваем
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * zoomSpeed
        );

        // плавно смещаем
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        // если почти достигли цели — телепорт в начало
        if (Vector3.Distance(transform.localPosition, targetPos) < 0.05f)
        {
            transform.localPosition = startPos;
            transform.localScale = startScale;
        }
    }
}
