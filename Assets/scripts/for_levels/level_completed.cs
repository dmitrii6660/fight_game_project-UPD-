using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level_completed : MonoBehaviour
{
    public GameObject targetUI;
    public TextMeshProUGUI[] options;
    private int currentIndex = 1;
    public GameObject endOfLevel;
    movement playerMove;
    public GameObject player;

    public Fade_script fade;

    private void optionSelectorHover()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && currentIndex != 0)
        {
            options[currentIndex].color = Color.white;
            currentIndex -= 1;
            options[currentIndex].color = Color.green;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && currentIndex != 1)
        {
            options[currentIndex].color = Color.white;
            currentIndex += 1;
            options[currentIndex].color = Color.green;
        }
    }

    private void optionSelector()
    {
        if(currentIndex == 1 && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("level2");
            Debug.Log("ure loaded level 2");
        }
        else if(currentIndex == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("level_menu");
            Debug.Log("ure loaded level menu");
        }
    }

    private void OnTriggerEnter2D()
    {
        targetUI.SetActive(false);
        StartCoroutine(transitionCoroutine());
    }

    private IEnumerator transitionCoroutine()
    {
        playerMove.enabled = false;
        fade.GetComponent<Fade_script>().FadeIn(0.5f);
        yield return new WaitForSeconds(1);
        endOfLevel.SetActive(true);
        player.transform.position = new Vector3(100, 0, 0);
        fade.GetComponent<Fade_script>().FadeOut(0.5f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = player.GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        optionSelectorHover();
        optionSelector();
    }
}
