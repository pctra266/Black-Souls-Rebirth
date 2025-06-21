using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Transform currentPoint;
    private Rigidbody2D rb;

    public float detectionRadius = 0.5f;
    public LayerMask bossLayer; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB;
    }

    public void Patrol(float speed)
    {
        Vector2 moveDirection = currentPoint == pointB ? Vector2.right : Vector2.left;
        rb.linearVelocity = moveDirection * speed;

        // Check overlap v?i v�ng nh? quanh point A ho?c B
        if (Physics2D.OverlapCircle(pointA.position, detectionRadius, bossLayer))
        {
            currentPoint = pointB;
        }
        else if (Physics2D.OverlapCircle(pointB.position, detectionRadius, bossLayer))
        {
            currentPoint = pointA;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointA.position, detectionRadius);
        Gizmos.DrawWireSphere(pointB.position, detectionRadius);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
