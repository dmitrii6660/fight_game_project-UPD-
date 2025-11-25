using System.Collections;
using UnityEngine;

public class spriteChanger : MonoBehaviour
{
    public GameObject legs;
    RotatePlayerWithHoldPoint rotateScript;
    public SpriteRenderer sr;
    public Sprite[] executeSprites;

    public Sprite[] hitSprites;

    public Sprite playerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotateScript = GetComponent<RotatePlayerWithHoldPoint>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && playerMode.playerIsExecuting == false)
        {
            StopAllCoroutines();
            StartCoroutine(attackCoroutine());
        }
    }
    private IEnumerator attackCoroutine()
    {
        sr.sprite = hitSprites[0];
        yield return new WaitForSeconds(0.2f);
        sr.sprite = playerSprite;
    }
}
