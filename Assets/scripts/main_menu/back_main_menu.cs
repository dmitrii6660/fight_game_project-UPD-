using UnityEngine;

public class back_main_menu : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject settings_menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            main_menu.SetActive(true);
            settings_menu.SetActive(false);
        }
    }
}
