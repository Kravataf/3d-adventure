using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Items/Weapon")]
public class WeaponDescriptor : ItemDescriptor
{
    [SerializeField, Min(1)] private int magazineSize = 30;
    public int MagazineSize => magazineSize;

    [SerializeField, Min(0)] private float range = 60f;
    public float Range => range;

    [SerializeField] private int damage = 10;
    public int Damage => damage;

    public override ItemSlotState CreateState() => new WeaponItemSlotState(magazineSize);
}
