using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ForestOfVengeance()
    {
        SceneManager.LoadScene("Forest of Vengeance");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
