using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private ItemDescriptor itemDescriptor;
    private ItemInstance itemInstance;

    private void Start()
    {
        if (itemInstance == null) itemInstance = Instance;
    }

    public ItemInstance Instance => itemInstance ??= new ItemInstance(itemDescriptor);

    public void Init(ItemInstance existing) => itemInstance = existing;

    public void CollectItem()
    {
        if (Inventory.instance.AddItem(Instance)) Destroy(gameObject);
    }
}
