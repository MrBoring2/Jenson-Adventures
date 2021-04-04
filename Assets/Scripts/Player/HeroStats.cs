using UnityEngine;
using UnityEngine.UI;

public class HeroStats : MonoBehaviour
{
    private int coins;
    private int diamonds;
    private int emeralds;
    public GameController gameController;
    public Player player;
    public int maxHealth=100;
    public int currentHealth;
    public Animator anim;
    public HealthBar healthBar;
    public Text healthText;
    GameObject[] enemyes;
    private int countCoins;
    private bool isDead = false;
    public Text countOfCouns;
    public Text countOfDiamonds;
    public Text countOfEmeralds;
    public Image coinImage;
    public DeathScreen deathScreen;
    Vector3 respawnPosition;
    int saveHealth;
    bool triger=false;

    public bool IsDead { get => isDead; set => isDead = value; }
    public int Coins { get => coins; set => coins = value; }
    public int Diamonds { get => diamonds; set => diamonds = value; }
    public int Emeralds { get => emeralds; set => emeralds = value; }
    public Vector3 RespawnPosition { get => respawnPosition; set => respawnPosition = value; }
    public int SaveHealth { get => saveHealth; set => saveHealth = value; }

    void Start()
    {
        IsDead = false;
        enemyes = GameObject.FindGameObjectsWithTag("Enemy");      
        healthBar.slider.enabled=false;
        currentHealth = maxHealth;
        player = FindObjectOfType<Player>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        healthText.text = currentHealth.ToString();
        anim = GetComponent<Animator>();
       
    }

    void Update()
    {
        healthBar.slider.maxValue = maxHealth;
        healthBar.slider.value = currentHealth;
        countOfCouns.text = Coins.ToString();
        countOfDiamonds.text = Diamonds.ToString() + "/3";
        countOfEmeralds.text = Emeralds.ToString() + "/2";
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
    void FixedUpdate()
    {
        //Обновление статуса здоровья
        if(currentHealth<=0)
        {
            anim.Play("AdventurePlayerDead");
        }
    }
    
    //Подбор монет

    //Метод смерти персонажа
    void Dead()
    {
        if (isDead)
        {
            triger = false;
            anim.SetBool("Respawn", false);
            //Вызов окна проигрыша
            gameController.LoseGame();
            //Остановка персонажа и всех врагов
            player.SetSpeed(0);
            anim.Play("AdventurePlayerDead");
            GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < go.Length; i++)
            {
                go[i].GetComponent<EnemiesScript>().speed = 0;
                go[i].GetComponent<EnemiesScript>().normalSpeed = 0;
            }
            gameController.Deaths++;    
            
        }      
    }

    public void ToCheckPoint()
    {

        anim.SetBool("Respawn", true);
        isDead = false;
        transform.position = RespawnPosition;
        player.SetSpeed(player.StartSpeed);
        currentHealth = SaveHealth;
        healthBar.SetHealth(currentHealth);
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<Animator>().SetBool("PlayerDead", false);
            go[i].GetComponent<EnemiesScript>().speed = go[i].GetComponent<EnemiesScript>().StartSpeed;
            go[i].GetComponent<EnemiesScript>().normalSpeed=go[i].GetComponent<EnemiesScript>().StartNormalSpeed;                  
        }  

    }


    //Метод получения урона
    public void TakeDamage(int damage)
    {
        //Проверка можно ли нанести урон, если здоровье больше 0 и нанесение урона
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthText.text = currentHealth.ToString()+"/"+maxHealth.ToString();
            healthBar.SetHealth(currentHealth);
            anim.SetInteger("Health", currentHealth);
            anim.SetTrigger("TakeDamage");
        }

        //Проверка закончилось ли здоровье у игрока
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
            healthBar.SetHealth(currentHealth);
            IsDead = true;
            anim.SetInteger("Health", currentHealth);
            Dead();
        }

        //Установка переменной для аниматора
        for (int i = 0; i < enemyes.Length; i++)
        {
            if (enemyes[i].gameObject)
            {
                enemyes[i].GetComponent<Animator>().SetBool("PlayerDead", IsDead);
            }
         
        }
    }

   
}
