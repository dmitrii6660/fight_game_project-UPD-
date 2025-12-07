using System.Collections;
using UnityEngine;

public class fadeIn_fadeOut : MonoBehaviour
{
    public Fade_script fade;

    private IEnumerator fadeCoroutine()
    {
        fade.GetComponent<Fade_script>().FadeIn(0.5f);
        yield return new WaitForSeconds(5f);
        fade.GetComponent<Fade_script>().FadeOut(0.5f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("fade is started");
        StartCoroutine(fadeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
