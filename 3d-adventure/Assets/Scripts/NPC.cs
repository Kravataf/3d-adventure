using UnityEngine;

public class NPC : Entity, IDamageable, IInteractable
{
    public void TakeDamage(int amount) => currentHealth -= amount;
    public void Interact()
    {
        print("dialog");
    }
}
