using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject itemSlot, itemList;
    [SerializeField] private TMP_Text currentWeight, itemName;

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
        foreach (var i in Inventory.instance.inv)
        {
            Instantiate(itemSlot, itemList.transform)
            .GetComponentInChildren<TMP_Text>().text 
            = $"{i.name} {i.weight}kg";
        }

        Inventory.instance.currentWeight = 0;
        foreach (var i in Inventory.instance.inv)
        {
            Inventory.instance.currentWeight += i.weight;
        }
    }
}
