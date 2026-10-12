using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button itemSlot, drop, equip;
    [SerializeField] private GameObject itemList;
    [SerializeField] private TMP_Text currentWeight, itemName;

    private void Awake()
    {
        drop.onClick.AddListener(() => ClickDrop());
        equip.onClick.AddListener(() => ClickEquip());
    }

    private void Update()
    {
        currentWeight.text = $"{Inventory.instance.currentWeight}/{Inventory.instance.maxWeight}kg";
    }

    public void Refresh()
    {
        foreach (Transform c in itemList.transform)
        {
            Destroy(c.gameObject);
        }

        for (int i = 0; i < Inventory.instance.inv.Count; i++)
        {
            int index = i;
            var slot = Instantiate(itemSlot, itemList.transform);
            slot.GetComponentInChildren<TMP_Text>().text = $"{Inventory.instance.inv[i].name} {Inventory.instance.inv[i].weight}kg";
            slot.onClick.AddListener(() => Inventory.instance.SelectItem(index));
        }

        itemName.text = Inventory.instance.selectedIndex > -1 ? Inventory.instance.selectedItem.name : "";

        Inventory.instance.currentWeight = 0;
        foreach (var i in Inventory.instance.inv)
        {
            Inventory.instance.currentWeight += i.weight;
        }
    }

    private void ClickDrop()
    {
        if (Inventory.instance.selectedIndex == -1) return;

        GameObject.Find("Player").GetComponent<PlayerInteract>().DropItem();
        Inventory.instance.RemoveItemAt(Inventory.instance.selectedIndex);
        Refresh();
    }

    private void ClickEquip()
    {
    }
}
