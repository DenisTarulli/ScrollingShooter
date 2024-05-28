using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private AudioSource music;
    [SerializeField] private float inGameVolume;
    [SerializeField] private float inPauseVolume;

    [HideInInspector] public bool gameIsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CheckPause();
    }

    public void CheckPause()
    {
        if (GameManager.Instance.gameIsOver) return;

        if (gameIsPaused)
        {
            BackSound();
            Resume();
        }
        else
            Pause();
    }

    public void Resume()
    {
        music.volume = inGameVolume;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        AudioManager.instance.Play("Pause");
        music.volume = inPauseVolume;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        ConfirmSound();
        Application.Quit();
    }

    public void ConfirmSound()
    {
        AudioManager.instance.Play("ClickUI");
    }

    public void BackSound()
    {
        AudioManager.instance.Play("BackUI");
    }
}
