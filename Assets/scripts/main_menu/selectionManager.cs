using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine.SceneManagement;
using UnityEngine;

public class selector : MonoBehaviour
{
    //settings and main menu (set active/disable)
    public GameObject main_menu;
    public GameObject settings_menu;

    //for text
    public TextMeshProUGUI[] options;

    int currentIndex = 2;

    void selectedOption()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 2)
        {
            SceneManager.LoadScene("level_menu");
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 1)
        {
            main_menu.SetActive(false);
            settings_menu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 0)
        {
            Application.Quit();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settings_menu.SetActive(false);
        main_menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        selectedOption();
        /*if (currentIndex != 2)
        {*/
            if (Input.GetKeyDown(KeyCode.UpArrow) && currentIndex != 2)
            {
                options[currentIndex].color = Color.white;
                currentIndex += 1;
                options[currentIndex].color = Color.green;
                Debug.Log("Upper");
                Debug.Log(currentIndex);
            }
        //}
        /*else if (currentIndex != 0)
        {*/
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentIndex != 0)
        {
            options[currentIndex].color = Color.white;
            currentIndex -= 1;
            options[currentIndex].color = Color.green;
            //selectedOption();
            Debug.Log(currentIndex);
        }
        //}    
    }
}
