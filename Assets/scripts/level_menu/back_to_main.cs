using UnityEngine;
using UnityEngine.SceneManagement;

public class back_into_menu : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ure in main menu");

            SceneManager.LoadScene("main_menu");
        }
    }

}
