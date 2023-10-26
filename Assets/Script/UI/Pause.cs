using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverPanel;

    void Update()
    {
        if (InputManager.GetInstance().GetPausePressed())
        {
            if (pauseMenu.activeSelf)
            {
                Unpause();
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void Unpause()
    {
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Retry()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
