using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject interactText, itemDrop;

    private void Update()
    {
        interactText.SetActive(false);

        RaycastHit hit;
        if (Physics.Raycast(head.position, head.forward, out hit, range, ~(1 << gameObject.layer)))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable == null) return;

            interactText.SetActive(true);
            if (InputSystem.actions["Interact"].WasPressedThisFrame()) interactable.Interact();
        }
    }

    public void DropItem() // toto by som mal dat do ineho scriptu
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
