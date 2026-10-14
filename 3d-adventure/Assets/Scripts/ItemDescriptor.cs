using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Items/Item")]
public class ItemDescriptor : ScriptableObject
{
    public string displayName;
    [TextArea] public string description;
    public Rarity rarity;
    [Min(0)] public float weight;

    [SerializeReference] public ItemSlotState stateTemplate;

    public virtual ItemSlotState CreateState() => null;
}

[System.Serializable]
public enum Rarity
{
    Abundant,
    Common,
    Uncommon,
    Rare,
    SuperRare,
    Legendary,
    Mythic,
    Godlike,
    Divine,
}