using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class start_level : MonoBehaviour
{
    movement playerMove;

      public Fade_script fade;
    public GameObject startTextes;
    public GameObject player;

    Rigidbody2D playerRB;
    public GameObject startAnimation;

    private void OnTriggerEnter2D()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMove.enabled = false;
        player.transform.position = new Vector3(50, 0, 0);
        startTextes.SetActive(true);
        startAnimation.SetActive(true);
        StartCoroutine(teleportingToLevel());
        StartCoroutine(fadeCoroutine());
    }

    private IEnumerator teleportingToLevel()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("level_1");
    }

     private IEnumerator fadeCoroutine()
    {
        fade.GetComponent<Fade_script>().FadeOut(0.5f);
        yield return new WaitForSeconds(5f);
        fade.GetComponent<Fade_script>().FadeIn(0.5f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = player.GetComponent<movement>();
        playerRB = player.GetComponent<Rigidbody2D>();
        //startTextes.SetActive(false);
        startAnimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
