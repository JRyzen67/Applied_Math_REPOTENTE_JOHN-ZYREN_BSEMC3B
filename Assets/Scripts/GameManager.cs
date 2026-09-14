using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int health;
    public int score;
    public int highScore;

    public int killedEnemyCount;

    public bool isLose;

    public GameState currentGameState;
    public UIManager uimanager;
    public EnemySpawner enemySpawner;
    public LevelController levelController;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (health <= 0)
        {
            isLose = true;
            uimanager.ShowGameOverUI();
            
        }

        //if (killedEnemyCount >= enemySpawner.maxEnemyToSpawn && health > 0)
        //{
        //    uimanager.ShowGameOverUI(false);
        //}

        if (score >= levelController.requireScoreToWin)
        {
            isLose = false;
            uimanager.ShowGameOverUI(false);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        LoadHighScore();
        SetGameState(GameState.Playing);
     
    }

    public void AddScore(int givenScore)
    {
        score += givenScore;
        uimanager.SetScoreUI();
        CheckHighScore();
    }

    public void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            uimanager.SetHighScoreUI();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore",highScore);
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore",0);
        uimanager.SetHighScoreUI();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
    }


  

    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }

    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
    }
}

public enum GameState
{
    Start,
    Playing,
    GameOver
}
