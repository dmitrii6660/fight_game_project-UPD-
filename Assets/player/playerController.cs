using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  // A, D или стрелки влево/вправ
        float moveVertical = Input.GetAxis("Vertical");      // W, S или стрелки вверх/вниз

        // Формируем вектор движения
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        // Перемещаем объект
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}


