using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button itemSlot;
    [SerializeField] private GameObject itemList;
    [SerializeField] private TMP_Text currentWeight, itemName, itemDescription;

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
            slot.GetComponentInChildren<TMP_Text>().text = $"{Inventory.instance.inv[i].itemDescriptor.name} ({Inventory.instance.inv[i].itemDescriptor.rarity}) {Inventory.instance.inv[i].itemDescriptor.weight}kg";
            slot.GetComponentInChildren<TMP_Text>().color = Inventory.instance.rarityColor[Inventory.instance.inv[i].itemDescriptor.rarity];
            slot.onClick.AddListener(() => Inventory.instance.SelectItem(index));
        }

        itemName.text = Inventory.instance.selectedIndex > -1 ? Inventory.instance.inv[Inventory.instance.selectedIndex].itemDescriptor.name : "";
        itemName.color = Inventory.instance.rarityColor[Inventory.instance.inv[Inventory.instance.selectedIndex].itemDescriptor.rarity];
        itemDescription.text = Inventory.instance.selectedIndex > -1 ? Inventory.instance.inv[Inventory.instance.selectedIndex].itemDescriptor.description : "";

        Inventory.instance.currentWeight = 0;
        foreach (var i in Inventory.instance.inv)
        {
            Inventory.instance.currentWeight += i.itemDescriptor.weight;
        }
    }

    public void ClickDrop()
    {
        if (Inventory.instance.selectedIndex == -1) return;
        if (Inventory.instance.inv.Count <= 0) return;

        GameObject.Find("Player").GetComponent<PlayerInteract>().DropItem();
        Inventory.instance.RemoveItemAt(Inventory.instance.selectedIndex);
        Refresh();
    }

    public void ClickEquip(bool isRight)
    {
        if (Inventory.instance.selectedIndex == -1) return;
        if (Inventory.instance.inv.Count <= 0) return;

        if (isRight)
        {
            if (Inventory.instance.selectedIndex == Inventory.instance.equippedIndexL) // swap L/R
            {
                Inventory.instance.equippedIndexL = Inventory.instance.equippedIndexR;
            }
            if (Inventory.instance.selectedIndex == Inventory.instance.equippedIndexR) Inventory.instance.equippedIndexR = -1; // toggle equip/unequip
            else Inventory.instance.equippedIndexR = Inventory.instance.selectedIndex;
        }
        else
        {
            if (Inventory.instance.selectedIndex == Inventory.instance.equippedIndexR) // swap L/R
            {
                Inventory.instance.equippedIndexR = Inventory.instance.equippedIndexL;
            }
            if (Inventory.instance.selectedIndex == Inventory.instance.equippedIndexL) Inventory.instance.equippedIndexL = -1; // toggle equip/unequip
            else Inventory.instance.equippedIndexL = Inventory.instance.selectedIndex;
        }
    }
}
