using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    //array for options
    public TextMeshProUGUI[] optionText;

    public GameObject pauseMenuUI;

    //getting current color
    public TextMeshProUGUI pause_menu_text;

    private Color currentColor;

    private int currentIndex = 2;

    void selectedOption()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 2)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; // return normal time
            Debug.Log("resume");
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 1)
        {
            Debug.Log("level rewind");
            pauseMenuUI.SetActive(false);
              Time.timeScale = 1f; // return normal time
            SceneManager.LoadScene("level_1");
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentIndex == 0)
        {
            Debug.Log("back to main menu");
            pauseMenuUI.SetActive(false);
              Time.timeScale = 1f; // return normal time
            SceneManager.LoadScene("main_menu");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //getting current color
        currentColor = pause_menu_text.color;
    }

    // Update is called once per frame
    void Update()
    {
        selectedOption();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentIndex != 0)
            {
                optionText[currentIndex].color = currentColor;
                currentIndex -= 1;
                optionText[currentIndex].color = new Color(255f, 0f, 189f);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentIndex != 2)
            {
                optionText[currentIndex].color = currentColor;
                currentIndex += 1;
                optionText[currentIndex].color = new Color(255f, 0f, 189f);
            }
        }
    }
}
