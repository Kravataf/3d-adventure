using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Item item;

    public void CollectItem()
    {
        bool hasCollected = Inventory.instance.AddItem(item);

        if (hasCollected)
        {
            Destroy(gameObject);
        }
    }
}
