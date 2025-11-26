using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour
{

    Vector3 startPosition;
    public Vector3 groundedRotationEuler = new Vector3(0, 0, 45f);

    // var for checking is item child obj or not
    private bool isHeld = false;

    // hit animation properties
    public float attackDuration = 0.25f; 
    public float attackAngle = 90f;     
    
    // for avoid many animations 
    private bool isAttacking = false;

    void Start()
    {
        CheckParent();
        startPosition = gameObject.transform.position;
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
            isHeld = false;

            //set world cordinates here
            transform.rotation = Quaternion.Euler(groundedRotationEuler);
        }
        else
        {  
            isHeld = true;
        }
    }

    void Update()
    {
        if(playerMode.playerIsDead == true)
        {
            gameObject.transform.position = startPosition;
        }
        //some code here
    }

    public bool Attack()
    {
        if (isAttacking)
        {
            return false; // dont start new hit animtion when current is not ended
        }

        // starting animatio coroutine
        StartCoroutine(AttackCoroutine());
        return true;
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        
        // storage start position -- PlayerPickup: Quaternion.identity)
        Quaternion startRotation = transform.localRotation;
        
        // setting last rotation (local positions)
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, attackAngle);
        
        float timer = 0f;
        
        // animation start
        while (timer < attackDuration)
        {
            float t = timer / attackDuration;
            // lerp for smooth animation
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);
            
            timer += Time.deltaTime;
            yield return null; 
        }
        
        // setting rotation to end point
        transform.localRotation = targetRotation; 
        
        // getting back start position
        timer = 0f;
        while (timer < attackDuration)
        {
            float t = timer / attackDuration;
            
            transform.localRotation = Quaternion.Lerp(targetRotation, startRotation, t);
            
            timer += Time.deltaTime;
            yield return null;
        }

        // setting rotation to start point
        transform.localRotation = startRotation;

        isAttacking = false;
    }
}