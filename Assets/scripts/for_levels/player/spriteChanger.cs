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
        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(hitCoroutine());
        }

        if(routiing.isExecute == true)
        {
            //StopAllCoroutines();
            StopCoroutine(executeCoroutine());
            StartCoroutine(executeCoroutine());
        }
    }

    private IEnumerator executeCoroutine()
    {
        rotateScript.enabled = false;
        legs.SetActive(false);
        sr.sprite = executeSprites[0];
        yield return new WaitForSeconds(1);
        sr.sprite = executeSprites[1];
        yield return new WaitForSeconds(1);
        sr.sprite = playerSprite;
        rotateScript.enabled = true;
        legs.SetActive(true);
        yield return null;
    }
    private IEnumerator hitCoroutine()
    {
        sr.sprite = hitSprites[0];
        yield return new WaitForSeconds(0.2f);
        sr.sprite = playerSprite;
    }
}
