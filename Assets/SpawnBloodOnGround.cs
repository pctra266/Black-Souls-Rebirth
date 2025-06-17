using UnityEngine;

public class SpawnBloodOnGround : MonoBehaviour
{
    public GameObject bloodPrefab;
    private bool hasSpawnedBlood = false;
    bool playerInRange = false;
    bool keyGiven = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Q) && !keyGiven)
        {
            keyGiven = true;
            InventoryManager.Instance.AddItem("Silver Key");
            Debug.Log("You got the key!");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasSpawnedBlood && collision.collider.CompareTag("Ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            // Get the bottom of the fallen knight
            Vector3 bottom = new Vector3(
                transform.position.x,
                transform.position.y - GetComponent<Collider2D>().bounds.extents.y,
                transform.position.z - 0.1f
            );
            GetComponent<Collider2D>().isTrigger = true;

            Instantiate(bloodPrefab, bottom, Quaternion.identity);
            hasSpawnedBlood = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
