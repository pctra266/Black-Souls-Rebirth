using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    private bool isGameOver = false;
    private bool isWin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        UploadScore();
    }

    public void AddScore(int point)
    {
        score += point;
        UploadScore();
    }

    public void UploadScore()
    {
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        isWin = true;
        Time.timeScale = 0;
        
        gameWinUI.SetActive(true);
    }


    public void Restart()
    {
        isGameOver = false;
        score = 0;
        UploadScore();
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }public bool IsGameWin()
    {
        return isWin;
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
