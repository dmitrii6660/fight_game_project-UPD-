using Unity.VisualScripting;
using UnityEngine;

public class next_floor : MonoBehaviour
{
    public GameObject player;

    public Transform spawn_point;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = spawn_point.position;
    }
}
