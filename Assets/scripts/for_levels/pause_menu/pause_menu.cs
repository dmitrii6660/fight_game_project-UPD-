using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    private bool gameIsPaused = false;
    //array for options
    public TextMeshProUGUI[] optionText;

    public GameObject pauseMenuUI;

    //getting current color
    public TextMeshProUGUI pause_menu_text;

    private Color currentColor;

    private int currentIndex = 2;

    private string currentSceneName;

    void selectedOption()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
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
            SceneManager.LoadScene(currentSceneName);
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
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        currentSceneName = SceneManager.GetActiveScene().name;
        //getting current color
        currentColor = pause_menu_text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused == true)
            {
                resume();
            }
            else
            {
                pause();
            }
        }

        if(gameIsPaused == true)
        {
            selectedOption();
            hover();
        }
    }

    void resume()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freezing time
        gameIsPaused = true;
    }

    void hover()
    {
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
