using UnityEngine;
using UnityEngine.SceneManagement;

public class goToCrashChamber : MonoBehaviour
{
    bool playerInRange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (playerInRange)
        {
            SceneManager.LoadScene(0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }
}
