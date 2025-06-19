using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        Vector2 direction = (currentPoint.position - transform.position);

        rb.linearVelocity = currentPoint == pointB.transform ? new Vector2(speed, 0) : new Vector2(-speed, 0);

        float distance = Vector2.Distance(transform.position, currentPoint.position);
        if (distance < 0.5f)
        {
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
