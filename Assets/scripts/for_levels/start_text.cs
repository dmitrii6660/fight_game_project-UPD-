using UnityEngine;
using System.Collections;
using TMPro; // Обязательно для TextMeshPro

public class start_text : MonoBehaviour
{
    // Ссылка на сам UI-объект TextMeshPro
    public GameObject temporaryTextObject; 
    public float displayDuration = 3f; // Время отображения в секундах
    public string initialMessage = "Добро пожаловать в игру!"; // Сообщение для отображения

    // Метод Start() вызывается один раз при первом кадре, если скрипт активен
    void Start()
    {
        // 2. Устанавливаем текст перед запуском
        TextMeshProUGUI tmpComponent = temporaryTextObject.GetComponent<TextMeshProUGUI>();

        // 3. Запускаем корутину для отображения и скрытия
        StartCoroutine(DisplayAndHideCoroutine());
    }

    private IEnumerator DisplayAndHideCoroutine()
    {
        // 1. Показываем текст
        temporaryTextObject.SetActive(true);

        // 2. Ждем заданное время
        yield return new WaitForSeconds(displayDuration);

        // 3. Скрываем текст
        temporaryTextObject.SetActive(false);
        
        // *Опционально: если этот контроллер больше не нужен, его можно уничтожить:*
        // Destroy(this.gameObject); 
    }
}