using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject itemSlot, itemList;

    public void Refresh()
    {
        foreach (Transform c in itemList.transform)
        {
            Destroy(c.gameObject);
        }
        foreach (var i in Inventory.instance.inv)
        {
            Instantiate(itemSlot, itemList.transform);
        }
    }
}
