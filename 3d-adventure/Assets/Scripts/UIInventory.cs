using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject itemSlot, itemList;
    [SerializeField] private TMP_Text currentWeight, itemName;

    private void Update()
    {
        currentWeight.text = $"{Inventory.instance.currentWeight}/{Inventory.instance.maxWeight}kg";

        /*
        na selectovanie treba aby kazdy item slot mal ref / nieco na svoj item
        nech mozem pouzivat tie itemy aj normalne (potom by mozno netrebalo ani
        destroyovat vsetko kazdy update...)
        */
    }

    public void Refresh()
    {
        foreach (Transform c in itemList.transform)
        {
            Destroy(c.gameObject);
        }
        foreach (var i in Inventory.instance.inv)
        {
            var iSlot = Instantiate(itemSlot, itemList.transform);
            iSlot.GetComponentInChildren<TMP_Text>().text = $"{i.name} {i.weight}kg";
        }

        Inventory.instance.currentWeight = 0;
        foreach (var i in Inventory.instance.inv)
        {
            Inventory.instance.currentWeight += i.weight;
        }
    }
}
