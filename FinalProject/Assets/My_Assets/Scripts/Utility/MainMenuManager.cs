using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public string PlayGameScene = "";
    public GameObject CreditsPanel;
    public GameObject LoadingScreen;
    public GameObject ControlsPanel;
    public Slider LoadingBar;

    private void Start()
    {
        if (CreditsPanel == null)
        {
            return;
        }
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadScene(PlayGameScene));
    }

    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }
    public void Controls()
    {
        ControlsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        CreditsPanel.SetActive(false);
    }
    public void Back()
    {
        ControlsPanel.SetActive(false);
    }

    IEnumerator LoadScene (string sceneName)
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


}//class
