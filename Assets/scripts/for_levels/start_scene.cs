using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using TMPro;

public class start_scene : MonoBehaviour
{
    public GameObject pts_text;
    public GameObject startText;
    public movement movement; // player movement script

    public GameObject mainCamera; // main camera
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pts_text.SetActive(false);
        movement.enabled = false;
        StartCoroutine(startGameCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator startGameCoroutine()
    {
        Debug.Log("camera curotine is started");
        yield return new WaitForSeconds(6f);
        Destroy(startText);
        mainCamera.transform.position = new Vector3(0, 0, 0);
        movement.enabled = true;
        pts_text.SetActive(true);
        Debug.Log("Camera new position: " + Camera.main.transform.position);

        //yield return null;
    }
    

}
