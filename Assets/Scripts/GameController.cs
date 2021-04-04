using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField] private NotEnouthCoinScreen notEnouthCoinScreen;
    [SerializeField] private CheckPointScreen checkPointScreen;
    [SerializeField] private DeathScreen deathScreen;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private GameObject upScreen;
    [SerializeField] private LevelEnd levelEnd;
    private int killsOfEnemies;
    private float compleate_time=0;
    private int deaths;

    private GameObject levelEndScreen;
    public HeroStats playerStats;
    public GameObject pauseMenu;
    public Canvas canvas;
    private Button retryButton;
    public Button checkPointButton;
    public Player player;

    bool pause = false;

    public bool Pause { get => pause; set => pause = value; }
    public float Compleate_time { get => compleate_time; set => compleate_time = value; }
    public int KillsOfEnemies { get => killsOfEnemies; set => killsOfEnemies = value; }
    public int Deaths { get => deaths; set => deaths = value; }

    // Start is called before the first frame update

    void Start()
    {
        //SaveSystem.LoadPlayer();
        retryButton = canvas.GetComponentInChildren<Button>();
        deathScreen.gameObject.GetComponent<Button>();
        Time.timeScale = 1;
        StartGame();
    }

    void Update()
    {
        Compleate_time += Time.deltaTime;
   
        if (Input.GetKeyDown(KeyCode.Escape) && Pause == false)
        {
            Pause = true;
            retryButton.enabled = false;
            checkPointButton.enabled = false;
            checkPointScreen.enabled = false;
            var mas = upScreen.GetComponentsInChildren<Button>();
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i].enabled = false;
            }
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Pause == true)
        {
            Pause = false;
            Time.timeScale = 1;          
            retryButton.enabled = true;
                checkPointButton.enabled = true;
                checkPointScreen.enabled = true;
            var mas = upScreen.GetComponentsInChildren<Button>();
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i].enabled = true;
            }
            pauseMenu.SetActive(false);
        }
    }
    public void UnPause()
    {
        Pause = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void LoadCheckPoint()
    {
        playerStats.ToCheckPoint();
        deathScreen.gameObject.SetActive(false);

    }
    public void StartGame()
    {
        deathScreen.gameObject.SetActive(false);
        StartCoroutine(StartRoutine());
    }
    public void LoseGame()
    {
        StartCoroutine(LoseRoutine());     
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
   
    private IEnumerator StartRoutine()
    {
        yield return StartCoroutine(startScreen.Fade(false));
        startScreen.gameObject.SetActive(false);
        Time.timeScale = 1;        
    }
    private IEnumerator LoseRoutine()
    {
        Time.timeScale = 1f;
        deathScreen.gameObject.SetActive(true);
        yield return StartCoroutine(deathScreen.Fade(true));
    }
    public IEnumerator NotEnouthCoin()
    {
        yield return new WaitForSeconds(0.5f);//StartCoroutine(notEnouthCoinScreen.Fade(false));
        notEnouthCoinScreen.gameObject.SetActive(false);
    }
    public IEnumerator WaitCheckPoint()
    {
        yield return new WaitForSeconds(0.5f);
        checkPointScreen.gameObject.SetActive(false);
    }
    public void CheckPoint()
    {
        checkPointScreen.gameObject.SetActive(true);
        StartCoroutine(WaitCheckPoint());
    }
    public void NotMoney()
    {
        notEnouthCoinScreen.gameObject.SetActive(true);
        StartCoroutine(NotEnouthCoin());
    }
    public void ToLoadLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    //public void SavePlayer()
    //{
    //    SaveSystem.SavePlayer(player, playerStats);
    //}
    //public void LoadPlayer()
    //{
    //    SaveData data =  SaveSystem.LoadPlayer();
    //    player.maxSpeed = data.maxspeed;
    //    player.damage = data.damage;
    //    player.attackRange = data.attackRadius;
    //    playerStats.maxHealth = data.maxhealth;

    //}
}
