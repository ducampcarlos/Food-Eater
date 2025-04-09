using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool gameStarted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Forzar orientación a landscape por seguridad
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void StartGame()
    {
        gameStarted = true;
        UIManager.Instance.HidePlayButton();
        ScoreManager.Instance.StartCounting();
    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void GameOver()
    {
        gameStarted = false;
        ScoreManager.Instance.StopCounting();
        UIManager.Instance.ShowGameOverScreen(ScoreManager.Instance.GetFinalScore());
    }
}
