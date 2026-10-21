using UnityEngine;

[System.Serializable]
public abstract class ItemSlotState
{
    public virtual ItemSlotState Clone() => (ItemSlotState)MemberwiseClone();
}

[System.Serializable]
public class WeaponItemSlotState : ItemSlotState
{
    [SerializeField] private int ammoCount;
    public int AmmoCount => ammoCount;

    public WeaponItemSlotState() {}
    public WeaponItemSlotState(int startingAmmo) => ammoCount = Mathf.Max(0, startingAmmo);

    public bool TryConsume(int amount)
    {
        if (amount < 0 || amount > ammoCount) return false;
        ammoCount -= amount;
        return true;
    }

    public int Reload(int amount, int magazineSize)
    {
        int added = Mathf.Max(0, Mathf.Min(amount, magazineSize - ammoCount));
        ammoCount += added;
        return added;
    }
}
