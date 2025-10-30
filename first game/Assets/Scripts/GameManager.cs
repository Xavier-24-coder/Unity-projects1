using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerController player;

    Image healthBar;
	Image bossHealthBarBG;
	Image bossHealthBar;

    GameObject pauseMenu;
    GameObject gameOverMenu;
    GameObject[] enemys;
    public bool enemysDie;
    public bool isPaused = false;

    public bool Gameover = false;

    public static GameManager Instance;

    private int enemyCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("BasicEnemy");
        enemyCount = enemies.Length;
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();
            healthBar = GameObject.FindGameObjectWithTag("UI_Health").GetComponent<Image>();
			bossHealthBar = GameObject.FindGameObjectWithTag("UI_BossHealth").GetComponent<Image>();
			bossHealthBarBG = GameObject.FindGameObjectWithTag("UI_BossHealthBG").GetComponent<Image>();

			pauseMenu = GameObject.FindGameObjectWithTag("pause");
            pauseMenu.SetActive(false);

            gameOverMenu = GameObject.FindGameObjectWithTag("gameover");
            gameOverMenu.SetActive(false);

            enemys = GameObject.FindGameObjectsWithTag("BasicEnemy");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            bossHealthBar.enabled = false;
            bossHealthBarBG.enabled = false;
        }
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag("BasicEnemy");
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            healthBar.fillAmount = (float)player.health / (float)player.maxHealth;

            if (player.health <= 0)
            {
                Gameover = true;
            }
            if (Gameover == true)
            {
                Time.timeScale = 0;
                gameOverMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
        }
       
    }
    public void EnemyDied()
    {
        enemyCount--;
        Debug.Log("Enemies remaining: " + enemyCount);

        if (enemyCount <= 0)
        {
            AllEnemiesDefeated();
        }
    }
    void AllEnemiesDefeated()
    {
        SceneManager.LoadScene(3);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    public void LoadTestS()
    {
        SceneManager.LoadScene(4);
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
