using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public bool IsDead => currentHealth <= 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy die");
        Destroy(gameObject); 
    }
}
