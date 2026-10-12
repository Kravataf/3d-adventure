using UnityEngine;

public class Player : Entity
{
    private void Start()
    {
        Item i = new Item() { name = "Sword", weight = 5 };
        Inventory.instance.AddItem(i);
    }
}
