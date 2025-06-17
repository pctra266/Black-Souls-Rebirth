using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public int requiredPresses = 3;
    private int currentPresses = 0;
    private bool playerNearby = false;
    private bool doorOpened = false;
    public GameObject PhysicalDoor;
    public GameObject fallenKnight;
    public Transform spawnPoint;

    void Update()
    {
        

        if (playerNearby && !doorOpened && Input.GetKeyDown(KeyCode.Q))
        {
            currentPresses++;
            Debug.Log("Pressed Q: " + currentPresses + "/" + requiredPresses);

            if (InventoryManager.Instance.HasItem("Silver Key"))
            {
                OpenDoor();
                return;
            }

            if (currentPresses >= requiredPresses)
            {
                TriggerFallenKnight();
            }
        }
    }

    void OpenDoor()
    {
        doorOpened = true;
        this.gameObject.SetActive(false);
        PhysicalDoor.SetActive(false);
    }


    void TriggerFallenKnight()
    {
        if (fallenKnight != null && spawnPoint != null)
        {
            Instantiate(fallenKnight, spawnPoint.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
