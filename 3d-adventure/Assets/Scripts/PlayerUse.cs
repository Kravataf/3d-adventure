using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUse : MonoBehaviour
{
    private void Update()
    {
        if (InputSystem.actions["RightHand"].WasPressedThisFrame())
        {
            if (Inventory.instance.equippedIndexR == -1 || Inventory.instance.inv.Count <= 0) return;

            var i = Inventory.instance.inv[Inventory.instance.equippedIndexR].itemDescriptor;

            if (i is WeaponDescriptor) print("pew (R)");
        }

        if (InputSystem.actions["LeftHand"].WasPressedThisFrame())
        {
            if (Inventory.instance.equippedIndexL == -1 || Inventory.instance.inv.Count <= 0) return;

            var i = Inventory.instance.inv[Inventory.instance.equippedIndexL].itemDescriptor;

            if (i is WeaponDescriptor) print("pew (L)");
        }
    }
}
