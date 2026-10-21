using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUse : MonoBehaviour
{
    [SerializeField] private Transform head;

    private void Update()
    {
        if (InputSystem.actions["RightHand"].WasPressedThisFrame())
        {
            if (Inventory.instance.equippedIndexR == -1 || Inventory.instance.inv.Count <= 0) return;
            Use(Inventory.instance.inv[Inventory.instance.equippedIndexR]);
        }

        if (InputSystem.actions["LeftHand"].WasPressedThisFrame())
        {
            if (Inventory.instance.equippedIndexL == -1 || Inventory.instance.inv.Count <= 0) return;
            Use(Inventory.instance.inv[Inventory.instance.equippedIndexL]);
        }
    }

    private void Use(ItemInstance item)
    {
        switch (item.itemDescriptor)
        {
            case WeaponDescriptor weapon when item.state is WeaponItemSlotState ammo:
                if (ammo.TryConsume(1))
                {
                    print("shot");
                    RaycastHit hit;
                    if (Physics.Raycast(head.position, head.forward, out hit, 60f, ~(1 << gameObject.layer)))
                    {
                        print("hit");
                        hit.collider.TryGetComponent<IDamageable>(out var damageable);
                        if (damageable != null) damageable.TakeDamage(weapon.Damage);
                    }
                }
                else
                {
                    print("no ammo");
                    return;
                }
            break;

            default:
                print("item used");
            break;
        }
    }
}
