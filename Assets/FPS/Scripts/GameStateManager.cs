using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public float gameTime = 30f;
    private float currentTime;

    public TMP_Text timerText;
    public GameObject winScreen;
    public GameObject loseScreen;

    private bool gameEnded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        currentTime = gameTime;

        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        UpdateTimerUI();
    }

    void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;

        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            LoseGame();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }

    public void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
