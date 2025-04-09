using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject playButton;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button restartButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void HidePlayButton()
    {
        playButton.SetActive(false);
    }

    public void ShowGameOverScreen(int finalScore)
    {
        scoreText.text = "Score: " + finalScore;
        gameOverPanel.SetActive(true);
    }
}
