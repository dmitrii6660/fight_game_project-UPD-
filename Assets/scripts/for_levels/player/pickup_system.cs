using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    // item position when player is holding it (EULERS cordinates) (X, Y, Z)
    public Vector3 heldItemRotation = new Vector3(0f, 0f, 15f);

    // hold point
    public Transform holdPoint; 
    
    // test key code
    public KeyCode interactKey = KeyCode.E;
    
    // temporarily storage item, whitch can pick
    private GameObject itemInRange;

    // item whitch is on player hand now
    private GameObject heldItem;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (heldItem == null && itemInRange != null)
            {
                Pickup();
            }
            else if (heldItem != null)
            {
                Drop();
            }
        }
        
        //for hit animation
        if (Input.GetMouseButtonDown(0) && heldItem != null)
        {
            AttemptAttack();
        }
    }

    // this method calling when player is in the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // checking if player has "player" tag and player dont hold anything
        if (other.CompareTag("Pickup") && heldItem == null)
        {
            itemInRange = other.gameObject;
        }
    }

    //if player is exit from trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == itemInRange)
        {
            itemInRange = null;
        }
    }

    // pickup logic
    private void Pickup()
    {
        Debug.Log("player gets: " + itemInRange.name);
        
        // item is now in player hands
        heldItem = itemInRange;
        
        // delete holding item from avaible itsems
        itemInRange = null;

        // MAIN LOGIC
        // making item -> child item for player obj
        heldItem.transform.SetParent(holdPoint);
        
        // set null item local position
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        heldItem.transform.localRotation = Quaternion.Euler(heldItemRotation); 

        // -- !WARNING! -- disble collider for avoid bugs
        heldItem.GetComponent<Collider2D>().enabled = false;
        
        // disable items physic
        Rigidbody2D itemRb = heldItem.GetComponent<Rigidbody2D>();
        if (itemRb != null)
        {
            itemRb.isKinematic = true;
        }
    }

    // drop down logic
    private void Drop()
    {
        // disabling from player obj (parent = null)
        heldItem.transform.SetParent(null);

        // enable collider
        heldItem.GetComponent<Collider2D>().enabled = true;

        // enable physic back
        Rigidbody2D itemRb = heldItem.GetComponent<Rigidbody2D>();
        if (itemRb != null)
        {
            itemRb.isKinematic = false; 
        }

        // hold item deleting from hold item storage
        heldItem = null;
    }
    public GameObject GetHeldItem()
    {
        return heldItem;
    }

    // method for animation
    private void AttemptAttack()
    {
        // getting component "ItemBehaviour" from item
        ItemBehaviour itemBehavior = heldItem.GetComponent<ItemBehaviour>();

        if (itemBehavior != null)
        {
            // calling public method Attack() on item
            itemBehavior.Attack();
        }
        else
        {
            Debug.LogWarning("item '" + heldItem.name + "'dont have script ItemBehaviour!");
        }
    }
}