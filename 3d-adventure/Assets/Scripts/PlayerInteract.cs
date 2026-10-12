using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform head;

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
}
