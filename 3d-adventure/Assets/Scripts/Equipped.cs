using TMPro;
using UnityEngine;

public class Equipped : MonoBehaviour
{
    [SerializeField] private TMP_Text equipped;

    private void Update()
    {
        equipped.text = "";
        
        if (Inventory.instance.inv.Count <= 0) return;
        if (Inventory.instance.equippedIndex == -1) return;

        equipped.text = Inventory.instance.inv[Inventory.instance.equippedIndex].name;
    }
}
