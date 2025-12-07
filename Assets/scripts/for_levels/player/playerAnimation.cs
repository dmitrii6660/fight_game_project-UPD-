using System.Collections;
using UnityEngine;

public class spriteChanger : MonoBehaviour
{
    private bool playerIsAttacking; // [FLAG] lyökö pelaaja nyt
    public GameObject legs;
    
    public SpriteRenderer sr;
    public Sprite[] executeSprites;

    public Sprite[] hitSprites;

    public Sprite playerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && playerMode.playerIsExecuting == false)
        {
            if(playerIsAttacking == false)
            {
                AudioManager.Instance.PlaySound("attackWithWeapon");
                StartCoroutine(attackCoroutine());
            }
        }
    }
    private IEnumerator attackCoroutine()
    {
        playerIsAttacking = true;
        sr.sprite = hitSprites[0];
        yield return new WaitForSeconds(0.2f);
        sr.sprite = playerSprite;
        playerIsAttacking = false;
    }
}
