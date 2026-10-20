using UnityEngine;

public class ItemDrop : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDescriptor itemDescriptor;
    private ItemInstance itemInstance;

    [SerializeField] private GameObject particle;

    private void Start()
    {
        if (itemInstance == null) itemInstance = Instance;
    }

    public ItemInstance Instance => itemInstance ??= new ItemInstance(itemDescriptor);

    public void Init(ItemInstance existing) => itemInstance = existing;

    public void Interact()
    {
        if (Inventory.instance.AddItem(Instance))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
