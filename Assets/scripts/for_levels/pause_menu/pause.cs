using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // menu here
    public static bool GameIsPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // return normal time
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freezing time
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
