using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startingLives;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject YouWinPanel;
    [SerializeField] private Castle playerCastle;
    [SerializeField] private Castle enemyCastle;

    [SerializeField] private TMP_Text playerLivesText;
    [SerializeField] private TMP_Text enemyLivesText;
    private int enemyCurrentLives;
    private int playerCurrentLives;

    private bool isPlaying;


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

    private void Awake()
    {
        isPlaying = true;
        PlayerCurrentLives = startingLives;
        EnemyCurrentLives = startingLives;
    }

    private void OnEnable()
    {
        playerCastle.OnCastleAttacked += OnCastleAttacked;
        enemyCastle.OnCastleAttacked += OnCastleAttacked;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isPlaying = !isPlaying;
            PausePanel.SetActive(!PausePanel.activeSelf);
            Time.timeScale = isPlaying ? 1 : 0;
        }
    }

    private void OnDestroy()
    {
        if (playerCastle != null) playerCastle.OnCastleAttacked -= OnCastleAttacked;
        if (enemyCastle != null) enemyCastle.OnCastleAttacked -= OnCastleAttacked;
    }

    private void OnCastleAttacked(object sender, EventArgs e)
    {
        if (ReferenceEquals(sender, playerCastle))
        {
            PlayerCurrentLives--;
            if (PlayerCurrentLives <= 0)
            {
                EndGame(GameOverPanel);
            }
        }
        else if (ReferenceEquals(sender, enemyCastle))
        {
            EnemyCurrentLives--;
            if (EnemyCurrentLives <= 0)
            {
                EndGame(YouWinPanel);
            }
        }
    }

    private void EndGame(GameObject panel)
    {
        isPlaying = false;
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}