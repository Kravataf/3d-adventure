using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject itemDrop, interactText;

    private void Update()
    {
        if (InputSystem.actions["Primary"].WasPressedThisFrame())
        {
        }

        if (InputSystem.actions["Secondary"].WasPressedThisFrame())
        {
        }

        RaycastHit hit;
        if (Physics.Raycast(head.position, head.forward, out hit, range, ~(1 << gameObject.layer)))
        {
            var itemDrop = hit.collider.GetComponent<ItemDrop>();

            interactText.SetActive(itemDrop);

            if (itemDrop)
            {
                if (InputSystem.actions["Interact"].WasPressedThisFrame())
                {
                    itemDrop.CollectItem();
                }
            }
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    public void DropItem()
    {
        Vector3 spawn;
        RaycastHit hit;
        if (Physics.SphereCast(head.position, 0.3f, head.forward, out hit, range / 2f, ~(1 << gameObject.layer),QueryTriggerInteraction.Ignore))
        {
            spawn = hit.point;
        }
        else
        {
            spawn = head.position + head.forward * range / 2f;
        }
        Instantiate(itemDrop, spawn, Quaternion.identity)
        .GetComponent<ItemDrop>().Init(Inventory.instance.inv[Inventory.instance.selectedIndex]);
    }
}
