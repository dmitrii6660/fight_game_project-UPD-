using UnityEngine;

public class RotatePlayerWithHoldPoint : MonoBehaviour
{
    public Transform spriteTransform;  // Спрайт игрока
    public Transform holdPoint;        // Точка держания предмета
    public float holdDistance = 1f;    // Расстояние, на которое предмет смещается вперед от игрока

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        if (spriteTransform == null || holdPoint == null) return;

        // Получаем позицию курсора в мировых координатах
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Для 2D z не нужен

        // Направление к курсору
        Vector3 direction = (mousePos - transform.position).normalized;

        // Угол поворота в градусах
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Вращаем спрайт игрока
        spriteTransform.rotation = Quaternion.Euler(10f, 10f, angle);

        // Вращаем holdPoint и смещаем вперед на holdDistance
        holdPoint.position = spriteTransform.position + direction * holdDistance;
        holdPoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
