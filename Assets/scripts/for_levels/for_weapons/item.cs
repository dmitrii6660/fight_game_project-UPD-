using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour
{
    private bool flipped = false;
    private Transform lastParent;
    public GameObject playerHoldPoint;
    public Vector3 heldItemRotation = new Vector3(0f, 0f, 15f);
    private Collider2D itemCollider;
    Vector3 startPosition;
    public Vector3 groundedRotationEuler = new Vector3(0, 0, 45f);

    // var for checking is item child obj or not
    private bool isHeld = false;

    // hit animation properties
    public float attackDuration = 0.25f; 
    public float attackAngle = 90f;     
    
    // for avoid many animations 
    private bool isAttacking = false;

    //for float up/down
    public float amplitude = 0.2f;   
    public float frequency = 2f;     

    private void floatUpDown()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z);
    }

    void Start()
    {
        lastParent = transform.parent;
        //itemCollider = GetComponent<Collider2D>();
        CheckParent();
        startPosition = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && transform.parent == null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("weapon is free");
        }
    }

    //unity funktio
    void OnTransformParentChanged()
    {
        CheckParent();
    }

    // functio for checking
    private void CheckParent()
    {
        if (transform.parent == null)
        {
            Debug.Log("enemy have a weapon");
            isHeld = false;
            gameObject.GetComponent<Collider2D>().enabled = true;
            //set world cordinates here
            startPosition = gameObject.transform.position;
            transform.rotation = Quaternion.Euler(groundedRotationEuler);
        }
        else
        {  
            Debug.Log("enemy dont have a weapon");
            gameObject.GetComponent<Collider2D>().enabled = false;
            //itemCollider.enabled = false;
            isHeld = true;
            transform.localRotation = Quaternion.Euler(heldItemRotation); 
        }
    }

    void Update()
    {
        if(transform.parent != lastParent)
        {
            lastParent = transform.parent;
            CheckParent();
        }
        if(isHeld == false)
        {
            floatUpDown();
        }
        //some code here
    }

    public void Attack()
{
    if (!isAttacking)
    {
        StartCoroutine(AttackCoroutine());
    }
}

private IEnumerator AttackCoroutine()
{
    isAttacking = true;

    // локальный стартовый (нулевой) поворот оружия
    Quaternion startRotation = Quaternion.identity;

    // поворот влево и вправо
    Quaternion leftRotation  = Quaternion.Euler(0, 0, attackAngle);      // например +90°
    Quaternion rightRotation = Quaternion.Euler(0, 0, -attackAngle);     // например -90°

    // выбираем направление поворота
    Quaternion from, to;

    if (!flipped)
    {
        // первый клик → поворот влево
        from = startRotation;
        to   = leftRotation;
    }
    else
    {
        // второй клик → поворот вправо
        from = leftRotation;
        to   = rightRotation;
    }

    float timer = 0f;

    while (timer < attackDuration)
    {
        float t = timer / attackDuration;
        transform.localRotation = Quaternion.Lerp(from, to, t);

        timer += Time.deltaTime;
        yield return null;
    }

    transform.localRotation = to;

    flipped = !flipped;  // переключаем состояние
    isAttacking = false;
}
}