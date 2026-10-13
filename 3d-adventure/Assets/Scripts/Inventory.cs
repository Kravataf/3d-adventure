using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> inv = new();
    public int currentWeight, maxWeight;

    [HideInInspector] public int selectedIndex = -1;

    [SerializeField] private UIInventory ui;

    private void Awake()
    {
        instance = this;
    }

    public bool AddItem(Item i)
    {
        if (currentWeight + i.weight <= maxWeight)
        {
            inv.Add(i);
            ui.Refresh();
            return true;
        }

        return false;
    }

    public void RemoveItemAt(int index)
    {
        inv.RemoveAt(index);
        if (index < selectedIndex) selectedIndex--;
        else if (index == selectedIndex) selectedIndex = Mathf.Min(selectedIndex, inv.Count - 1);
    }

    public void SelectItem(int index)
    {
        selectedIndex = (index >= 0 && index < inv.Count) ? index : -1;
        ui.Refresh();
    }
}

[System.Serializable]
public struct Item
{
    public string name;
    public int weight;
    public string description;
}