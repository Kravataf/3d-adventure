using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUse : MonoBehaviour
{
    [SerializeField] private Transform head;

    private void Update()
    {
        var inventory = Inventory.instance;

        if (InputSystem.actions["RightHand"].WasPressedThisFrame()) TryUse(inventory.equippedIndexR);
        if (InputSystem.actions["LeftHand"].WasPressedThisFrame()) TryUse(inventory.equippedIndexL);
    }

    private void TryUse(int index)
    {
        var inv = Inventory.instance.inv;
        if (index < 0 || index >= inv.Count) return;

        Use(inv[index]);
    }

    private void Use(ItemInstance item)
    {
        switch (item.itemDescriptor)
        {
            case WeaponDescriptor weapon when item.state is WeaponItemSlotState ammo:
                if (ammo.TryConsume(1))
                {
                    print($"shot ({ammo.AmmoCount} / {weapon.MagazineSize})");
                    RaycastHit hit;
                    if (Physics.Raycast(head.position, head.forward, out hit, 60f, ~(1 << gameObject.layer)))
                    {
                        print("hit");
                        if (hit.collider.TryGetComponent<IDamageable>(out var damageable)) damageable.TakeDamage(weapon.Damage);
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
