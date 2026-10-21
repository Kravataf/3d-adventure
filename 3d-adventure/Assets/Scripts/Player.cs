using UnityEngine;

public class Player : Entity, IDamageable
{
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Instantiate(particleBlood, transform.position, Quaternion.identity);
        if (currentHealth <= 0) Death();
    }

    public void Death()
    {}
}
