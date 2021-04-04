using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    //[SerializeField] Button retryButton;
    public GameObject LevelEndScreen;
    public GameController game;
    private Player player;
    private HeroStats heroStats;
    float alpha;
    private CanvasGroup canvasGroup;
    public Text countOfCoins;
    public Text countOfDiamonds;
    public Text countOfEmeralds;
    public Text time;
    public Text kills;
    public Text deaths;

    private void Awake()
    {
        canvasGroup = LevelEndScreen.GetComponent<CanvasGroup>();
        alpha = 0;
       // retryButton.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        heroStats = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            
            int minutes = (int)game.Compleate_time / 60;
            int seconds = (int)game.Compleate_time % 60;
            countOfCoins.text = heroStats.Coins.ToString();
            countOfDiamonds.text = heroStats.Diamonds.ToString()+"/3";
            countOfEmeralds.text = heroStats.Emeralds.ToString()+"/2";
            time.text = "Время прохождения: "+minutes+":"+seconds;
            kills.text = "Побежденов врагов: " + game.KillsOfEnemies;
            deaths.text = "Смертей: " + game.Deaths;
            EndGame();
            player.SetSpeed(0);
            GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < go.Length; i++)
            {
                go[i].GetComponent<EnemiesScript>().speed = 0;
                go[i].GetComponent<EnemiesScript>().normalSpeed = 0;
            }          
        }
    }
    public IEnumerator Fade(bool toVisible)
    {

        float step = toVisible ? 0.01f : -0.01F;
        int endValue = toVisible ? 1 : 0;

        while (alpha != endValue)
        {
            alpha += step;
            canvasGroup.alpha = alpha;
            if (alpha < 0)
            {
                alpha = 0;
            }
            else if (alpha > 1)
            {
                alpha = 1;
            }
            yield return new WaitForSeconds(0.01f);
        }

    }
    private IEnumerator EndRoutine()
    {
        Time.timeScale = 1f;
        LevelEndScreen.SetActive(true);
        yield return StartCoroutine(Fade(true));
        Time.timeScale = 0;
    }
    public void EndGame()
    {
        StartCoroutine(EndRoutine());
        
        
    }
}
