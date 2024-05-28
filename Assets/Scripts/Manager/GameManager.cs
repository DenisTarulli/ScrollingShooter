using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private AudioSource music;

    [HideInInspector] public bool gameIsOver;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameIsOver = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            GameOver();
    }

    public void GameOver()
    {
        gameIsOver = true;
        Cursor.lockState = CursorLockMode.None;

        music.volume = 0f;

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        PlayerCombat player = FindObjectOfType<PlayerCombat>();

        if (player.CurrentHealth > 0)
        {
            winText.SetActive(true);
            AudioManager.instance.Play("Win");
        }
        else
        {
            AudioManager.instance.Play("Lose");
            loseText.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
