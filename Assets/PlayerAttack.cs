using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public float startTimeBetweenAttack = 0.5f;

    private Animator animator;
    public Transform attackPos;
    public float attackRange = 0.5f;
    public LayerMask whatIsEnemies;
    public int damage = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("attack");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (var enemy in enemiesToDamage)
                {
                    enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
                }

                timeBetweenAttack = startTimeBetweenAttack;
            }
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackPos != null)
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
