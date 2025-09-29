using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerController player;

    Image healthBar;

    GameObject pauseMenu;

    public bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();
            healthBar = GameObject.FindGameObjectWithTag("UI_Health").GetComponent<Image>();

            pauseMenu = GameObject.FindGameObjectWithTag("pause");
            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;

            pauseMenu.SetActive(true);

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
            Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void MainMenu()
    {
        LoadLevel(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;

            pauseMenu.SetActive(false);

            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
