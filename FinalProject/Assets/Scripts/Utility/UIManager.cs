using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    private PlayerStats _playerStats;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        gameOverScreen.SetActive(false);
    }

    private void FixedUpdate()
    {
        GameOver();
    }

    void GameOver()
    {
        if (_playerStats.GameOver == true)
        {
            Time.timeScale = 0.0f;
            gameOverScreen.SetActive(true);
        }
        else
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        _playerStats.GameOver = false;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
