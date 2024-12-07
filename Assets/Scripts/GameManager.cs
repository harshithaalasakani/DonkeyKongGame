using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int lives { get; private set; } = 3;

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

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        ScoreManager.Instance.ResetScore();
        LoadLevel();
    }

    private void LoadLevel()
    {
        Camera camera = Camera.main;

        // Simple transition effect
        if (camera != null)
        {
            camera.cullingMask = 0;
        }

        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Level01"); // Replace with your scene name
        Invoke(nameof(StartLevel), 0.5f); // Delay to ensure scene is loaded
    }

    private void StartLevel()
    {
        // Start the timer after the level has loaded
        ScoreManager.Instance.StartTimer();
    }

    public void LevelComplete()
    {
        ScoreManager.Instance.StopTimer(); // Stop the timer when level is complete
        LoadLevel();
    }

    public void LevelFailed()
    {
        lives--;

        if (lives <= 0)
        {
            NewGame();
        }
        else
        {
            ScoreManager.Instance.StopTimer();
            ScoreManager.Instance.ResetScore();
            LoadLevel();
        }
    }
}
