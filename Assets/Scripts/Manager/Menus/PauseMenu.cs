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
    private PlayerInputs playerInputsActions;

    private void Awake()
    {
        playerInputsActions = new PlayerInputs();
        playerInputsActions.Player.Enable();
        playerInputsActions.Player.Pause.performed += CheckPause;
    }

    public void CheckPause(InputAction.CallbackContext context)
    {
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
