using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> inv = new();
    public int currentWeight, maxWeight;

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
}

public struct Item
{
    public string name;
    public int weight;
}