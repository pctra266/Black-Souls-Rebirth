using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageDealer
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed = 2f;
    public int damage = 1;
    private float dazedTime;
    public float startDazedTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 2;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

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

    public void setDazedTime()
    {
        dazedTime = startDazedTime;
    }

    public int GetDamageAmount()
    {
        return damage;
    }
}
