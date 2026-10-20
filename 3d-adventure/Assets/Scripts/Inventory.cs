using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemInstance> inv = new();
    public float currentWeight, maxWeight;

    [HideInInspector] public int selectedIndex = -1, equippedIndexL = -1, equippedIndexR = -1;
    [SerializeField] private UIInventory ui;

    [HideInInspector] public Dictionary<Rarity, Color> rarityColor = new Dictionary<Rarity, Color>
    {
        { Rarity.Abundant,  Color.black  },
        { Rarity.Common,    Color.gray   },
        { Rarity.Uncommon,  Color.green  },
        { Rarity.Rare,      Color.blue   },
        { Rarity.SuperRare, Color.purple },
        { Rarity.Legendary, Color.yellow },
        { Rarity.Mythic,    Color.red    },
        { Rarity.Godlike,   Color.pink   },
        { Rarity.Divine,    Color.white  }
    };

    private void Awake()
    {
        instance = this;
    }

    public bool AddItem(ItemDescriptor itemDescriptor) => AddItem(new ItemInstance(itemDescriptor));

    public bool AddItem(ItemInstance itemInstance)
    {
        if (currentWeight + itemInstance.itemDescriptor.weight <= maxWeight)
        {
            inv.Add(itemInstance);
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
        if (index < equippedIndexR) equippedIndexR--;
        else if (index == equippedIndexR) equippedIndexR = -1;
    }

    public void SelectItem(int index)
    {
        selectedIndex = (index >= 0 && index < inv.Count) ? index : -1;
        ui.Refresh();
    }
}