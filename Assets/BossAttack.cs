using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public void Attack(Transform player, float speed)
    {
        if (player == null) return;

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Add attack logic here (animation, damage, etc.)
    }
}
