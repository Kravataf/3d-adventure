using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject itemDrop;

    private void Update()
    {
        if (InputSystem.actions["Interact"].WasPressedThisFrame())
        {
            RaycastHit hit;
            if (Physics.Raycast(head.position, head.forward, out hit, range, ~(1 << gameObject.layer)))
            {
                var itemDrop = hit.collider.GetComponent<ItemDrop>();

                if (itemDrop) itemDrop.CollectItem();
            }
        }
    }

    public void DropItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(head.position, head.forward, out hit, range / 2f, ~(1 << gameObject.layer)))
        {
            var item = Instantiate(itemDrop, hit.point, Quaternion.identity);
            item.GetComponent<ItemDrop>().item = Inventory.instance.inv[Inventory.instance.selectedIndex];
        }
        else
        {
            var item = Instantiate(itemDrop, head.position + head.forward * range / 2f, Quaternion.identity);
            item.GetComponent<ItemDrop>().item = Inventory.instance.inv[Inventory.instance.selectedIndex];
        }
    }
}
