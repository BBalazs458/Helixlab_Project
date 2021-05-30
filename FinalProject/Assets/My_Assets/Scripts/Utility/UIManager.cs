using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseMenuScreen;
    [SerializeField] GameObject objectivesScreen;
    [SerializeField] Slider _healthSlider;
    [SerializeField] Image _painHud;

    public GameObject LoadingScreen;
    public Slider LoadingBar;

    private PlayerStats _playerStats;
    private AudioListener _audioListener;

    //private bool isMouseLock = true;
    private bool _gameIsPaused = false;

    private void Awake()
    {
        _audioListener = FindObjectOfType<AudioListener>();
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        Time.timeScale = 0.0f;
        objectivesScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (_gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseMenu();
            }

            
        }
       StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        if (_playerStats.GameOver == true)
        {
            _audioListener.enabled = false;
            yield return new WaitForSeconds(3f);
            Time.timeScale = 0.0f;
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

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
        //SceneManager.LoadSceneAsync("MainMenu");
        StartCoroutine(LoadScene("MainMenu"));
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _audioListener.enabled = true;
        _gameIsPaused = false;
        pauseMenuScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Pause menu
    public void PauseMenu()
    {
        Time.timeScale = 0.0f;
        _audioListener.enabled = false;
        _gameIsPaused = true;
        pauseMenuScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OkButton()
    {
        objectivesScreen.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Update health
    public void UpdateHelathBar(int current)
    {
        _healthSlider.value = current;
    }

    public void SetMaximumHealth(int current)
    {
        _healthSlider.maxValue = current;
    }

    public void SetPainHudTransparent(int health)
    {
        if (health <= 70 && health > 50)
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 0.2f);
        }
        else if (health <= 50 && health > 20)
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 0.5f);
        }
        else if (health <= 20)
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 1);
        }
        else
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 0);
        }
    }




    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBar.value = progress;

            yield return null;//wait the last frame
        }

    }


    //void LockMouse()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        isMouseLock = !isMouseLock;
    //    }
    //    //True lock // False unlock
    //    if (isMouseLock)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //}
}
