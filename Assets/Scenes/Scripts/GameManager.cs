using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isGameOver { get; private set; }
    public float score { get; private set; }
    public float scoreSpeed = 10f;

    [Header("Speed Settings")]
    public float initialSpeedMultiplier = 1f;
    public float maxSpeedMultiplier = 2.5f;
    public float speedIncreaseRate = 0.02f;
    public float GameSpeedMultiplier { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    void Awake()
    {
        Time.timeScale = 1f;

        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GameSpeedMultiplier = initialSpeedMultiplier;
    }

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;
        score += scoreSpeed * Time.deltaTime;

        if (scoreText != null)
        {
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        }
        if (GameSpeedMultiplier < maxSpeedMultiplier)
        {
            GameSpeedMultiplier += speedIncreaseRate * Time.deltaTime;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
