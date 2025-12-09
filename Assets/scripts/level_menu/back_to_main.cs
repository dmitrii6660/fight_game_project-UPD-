using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class back_into_menu : MonoBehaviour
{
    public FadeController fade;

    private IEnumerator teleportingCoroutine()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("main_menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(teleportingCoroutine());
        }
    }

}
