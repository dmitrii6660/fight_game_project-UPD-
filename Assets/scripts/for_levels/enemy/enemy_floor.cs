using UnityEngine;

public class TrackableItem : MonoBehaviour
{
    void Start()
    {
        // when game starts adding to enemy_list
        //CollectableManager.Instance.RegisterItem(this.gameObject);
    }

    // when enemy is destroyed
    public void DestroyItem()
    {
        // calling to enemy_managr thats obj(this enemy) is destroyed
        CollectableManager.Instance.ItemDestroyed(this.gameObject);

        // destroying current obj
        Destroy(this.gameObject);
    }
}