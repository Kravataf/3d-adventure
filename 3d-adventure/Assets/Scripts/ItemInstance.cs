using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemDescriptor itemDescriptor;
    [SerializeReference] public ItemSlotState state;

    public ItemInstance(ItemDescriptor itemDescriptor)
    {
        this.itemDescriptor = itemDescriptor;
        state = itemDescriptor.CreateState();
    }
}
