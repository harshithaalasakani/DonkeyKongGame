using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score { get; private set; } = 0;
    public TextMeshProUGUI scoreText; // Link your TextMeshPro text element here in the Inspector

    private float timeScore = 0f;
    private bool isTimerRunning = false;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            // Update score based on time elapsed, rounding to whole seconds
            timeScore += Time.deltaTime;
            score = Mathf.FloorToInt(timeScore);

            UpdateScoreText();
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timeScore = 0;
        UpdateScoreText();
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetScore()
    {
        timeScore = 0;
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Time: " + score;
        }
    }
}
