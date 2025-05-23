using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startingLives;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject YouWinPanel;
    [SerializeField] private Castle playerCastle;
    [SerializeField] private Castle enemyCastle;
    
    [SerializeField] private TMPro.TMP_Text playerLivesText;
    [SerializeField] private TMPro.TMP_Text enemyLivesText;

    private int playerCurrentLives;
    private int enemyCurrentLives;
    bool isPlaying;

    private static GameManager instance;

    public int PlayerCurrentLives
    {
        get => playerCurrentLives;
        set
        {
            playerCurrentLives = value;
            playerLivesText.text = playerCurrentLives.ToString();
        }
    }
    
    public int EnemyCurrentLives
    {
        get => enemyCurrentLives;
        set
        {
            enemyCurrentLives = value;
            enemyLivesText.text = enemyCurrentLives.ToString();
        }
    }
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return instance;
        }
    }
    private void Awake()
    {
        isPlaying = true;
        PlayerCurrentLives = startingLives;
        EnemyCurrentLives = startingLives;
        instance = this;
        
        playerCastle.OnCastleAttacked += PlayerLoseLife;
        enemyCastle.OnCastleAttacked += EnemyLoseLife;
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isPlaying = !isPlaying;
            PausePanel.SetActive(!PausePanel.activeSelf);
            Time.timeScale = isPlaying ? 1 : 0;
        }
    }

    private void PlayerLoseLife(object sender, EventArgs e)
    {
        PlayerCurrentLives--;

        if (PlayerCurrentLives <= 0)
        {
            isPlaying = false;
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void EnemyLoseLife(object sender, EventArgs e)
    {
        EnemyCurrentLives--;

        if (EnemyCurrentLives <= 0)
        {
            isPlaying = false;
            YouWinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    public void RestartScene()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MenuScene()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }
}
