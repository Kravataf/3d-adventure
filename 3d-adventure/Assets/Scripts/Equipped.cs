using TMPro;
using UnityEngine;

public class Equipped : MonoBehaviour
{
    [SerializeField] private TMP_Text equipped;

    private void Update()
    {
        var inventory = Inventory.instance;
        var inv = inventory.inv;

        equipped.text = $"L {Describe(inventory.equippedIndexL)} R {Describe(inventory.equippedIndexR)}";

        if (inv.Count <= 0) return;
    }

    private string Describe(int index)
    {
        var inv = Inventory.instance.inv;
        if (index < 0 || index >= inv.Count) return "--";

        var desc = inv[index].itemDescriptor;

        string extra;
        switch (desc)
        {
            case WeaponDescriptor weapon when inv[index].state is WeaponItemSlotState ammo:
                extra = $"({ammo.AmmoCount} / {weapon.MagazineSize})";
            break;

            default:
                extra = "";
            break;
        }
        
        return $"{desc.displayName} ({desc.rarity}) {extra}";
    }
}
