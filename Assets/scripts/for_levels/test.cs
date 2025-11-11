using UnityEngine;

public class testCB : MonoBehaviour
{
    private bool InTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InTrigger = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        InTrigger = true;
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D()
    {
        InTrigger = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && InTrigger == true)
        {
            Destroy(gameObject);
        }
    }
}
