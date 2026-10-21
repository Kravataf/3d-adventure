using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth, currentHealth; // healthbar to pouziva takze to nemoze byt protected

    [SerializeField] protected GameObject particleBlood;
}
