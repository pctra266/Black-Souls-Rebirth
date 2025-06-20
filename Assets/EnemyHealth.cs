using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public GameObject bloodEffect;
    private EnemyController enemyController;
    public bool IsDead => currentHealth <= 0;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        enemyController.setDazedTime();
        currentHealth -= amount;
        Debug.Log($"Enemy took {amount} damage, current health: {currentHealth}");

        Vector3 spawnPos = transform.position;
        spawnPos.z = 0f; 

        GameObject effect = Instantiate(bloodEffect, spawnPos, Quaternion.identity);
        Destroy(effect, 1f);

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
