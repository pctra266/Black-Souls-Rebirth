using UnityEngine;

[RequireComponent(typeof(BossPatrol), typeof(BossAttack))]
public class BossController : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float attackSpeed = 4f;
    public float attackRange = 15f;

    private BossPatrol patrol;
    private BossAttack attack;
    private Transform player;
    private bool playerInRange;

    private void Awake()
    {
        patrol = GetComponent<BossPatrol>();
        attack = GetComponent<BossAttack>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please tag your player object as 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        playerInRange = distance <= attackRange;

        if (playerInRange)
        {
            attack.Attack(player, attackSpeed);
        }
        else
        {
            patrol.Patrol(patrolSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
