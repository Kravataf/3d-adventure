using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private GameObject itemDrop;

    private void Update()
    {
        if (InputSystem.actions["Interact"].WasPressedThisFrame())
        {
            RaycastHit hit;
            if (Physics.Raycast(head.position, head.forward, out hit, 60f))
            {
                var itemDrop = hit.collider.GetComponent<ItemDrop>();

                if (itemDrop) itemDrop.CollectItem();
            }
        }
    }

    public void DropItem()
    {
        Instantiate(itemDrop, head.position, Quaternion.identity);
    }
}
