using UnityEngine;
using System.Collections; 

public class FlickerEffect : MonoBehaviour
{
    [Header("effect settings")]
   
    // effect properties
    public float totalDuration = 1f; 
    
    public int flickerCount = 10; 

    // link to obj whitch must flicking
    private GameObject targetObject;

    void Start()
    {
        targetObject = this.gameObject;
        this.gameObject.SetActive(true);
        SpriteRenderer sr = targetObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // public method 
    public void StartFlicker()
    {
        //this.gameObject.SetActive(true);
        StopAllCoroutines(); 
        StartCoroutine(FlickerCoroutine());
    }

    private IEnumerator FlickerCoroutine()
    {
        Debug.Log("flick statrted");

        // for better speed / flicker count
        float singleDuration = totalDuration / (float)(flickerCount * 2);
        
    
    SpriteRenderer sr = targetObject.GetComponent<SpriteRenderer>();

    for (int i = 0; i < flickerCount; i++)
    {
      
        sr.enabled = true; // ! using sr.enabled=true/false without setActive !
        yield return new WaitForSeconds(singleDuration);
        
        sr.enabled = false;
        yield return new WaitForSeconds(singleDuration);
    }


    sr.enabled = false; // setting off effect
    }
}