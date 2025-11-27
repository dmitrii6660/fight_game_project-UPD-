using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class target : MonoBehaviour
{

    public GameObject targetUI;

    public TextMeshProUGUI targetText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            targetText.text = "go into car";
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(level_storage.isLevelCleared)
        {
            targetUI.SetActive(true);
            targetText.text = "find a case";
        }
        
    }
}
