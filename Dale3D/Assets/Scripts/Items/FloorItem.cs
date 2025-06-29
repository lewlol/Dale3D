using UnityEngine;

public class FloorItem : MonoBehaviour
{
    public Item item;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Inventory>().AddItem(item, 1);
            Destroy(gameObject);
        }
    }
}
