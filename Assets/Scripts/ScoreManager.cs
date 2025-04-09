using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public float score = 0f;
    public TextMeshProUGUI scoreText;
    public float scoreRate = 10f; // puntos por segundo

    private bool isCounting = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isCounting)
        {
            score += scoreRate * Time.deltaTime;
            UpdateUI();
        }
    }

    public void AddScore(float amount)
    {
        score += amount;
        UpdateUI();
    }

    public void StartCounting()
    {
        isCounting = true;
    }

    public void StopCounting()
    {
        isCounting = false;
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public int GetFinalScore()
    {
        return Mathf.FloorToInt(score);
    }
}
