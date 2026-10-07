using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject fill;
    [SerializeField] private Player player;

    private void Update()
    {
        var health = player.currentHealth / player.maxHealth;
        fill.transform.localScale = new Vector2(health, 1f);
    }
}
