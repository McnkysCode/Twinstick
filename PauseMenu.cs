using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public static bool IsGamePaused = false;
    [SerializeField] private GameObject PauseMenuUI;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game D:<");
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu screen");
    }
}
