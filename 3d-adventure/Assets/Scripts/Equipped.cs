using TMPro;
using UnityEngine;

public class Equipped : MonoBehaviour
{
    [SerializeField] private TMP_Text equipped;

    private void Update()
    {
        equipped.text = "Left: -- Right: --";

        if (Inventory.instance.inv.Count <= 0) return;

        equipped.text = "Left: ";
        if (Inventory.instance.equippedIndexL > -1) equipped.text += $"{Inventory.instance.inv[Inventory.instance.equippedIndexL].itemDescriptor.name} ({Inventory.instance.inv[Inventory.instance.equippedIndexL].itemDescriptor.rarity}) ";
        else equipped.text += "-- ";
        equipped.text += "Right: ";
        if (Inventory.instance.equippedIndexR > -1) equipped.text += $"{Inventory.instance.inv[Inventory.instance.equippedIndexR].itemDescriptor.name} ({Inventory.instance.inv[Inventory.instance.equippedIndexR].itemDescriptor.rarity})";
        else equipped.text += "--";
    }
}
