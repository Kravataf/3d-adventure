using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Items/Weapon")]
public class WeaponDescriptor : ItemDescriptor
{
    [SerializeField, Min(1)] private int magazineSize = 30;
    public int MagazineSize => magazineSize;

    public override ItemSlotState CreateState() => new WeaponItemSlotState(magazineSize);
}
