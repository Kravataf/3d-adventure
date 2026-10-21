using UnityEngine;

public class NPC : Entity, IDamageable, IInteractable
{
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Death();
    }
    
    public void Interact()
    {
        print("dialog");
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
