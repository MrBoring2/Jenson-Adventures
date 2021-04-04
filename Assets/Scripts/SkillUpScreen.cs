using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUpScreen : MonoBehaviour
{
    [SerializeField] private NotEnouthCoinScreen notEnouthCoinScreen;
    [SerializeField] private GameObject game;
    private GameController gamecontrol;
    public Text curHealth;
    public Text curDamage;
    public Text curSpeed;
    public Text curAttackRange;
    public Text priceHealth;
    public Text priceDamage;
    public Text priceSpeed;
    public Text priceAttackRange;
    private HeroStats stats;
    private Player player;
    private int curMoney;
    public int pricePerHealth;
    public int pricePerDamage;
    public int pricePerSpeed;
    public int pricePerAttackRange;
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gamecontrol = game.GetComponent<GameController>();
        curMoney = stats.Coins;
        priceHealth.text = pricePerHealth.ToString();
        priceDamage.text = pricePerDamage.ToString();
        priceSpeed.text = pricePerSpeed.ToString();
        priceAttackRange.text = pricePerAttackRange.ToString();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        curMoney = stats.Coins;
        curHealth.text = stats.maxHealth.ToString();
        curDamage.text = player.damage.ToString();
        curSpeed.text = player.maxSpeed.ToString();
        curAttackRange.text = player.attackRange.ToString();
    
    }
    public void AddHealth()
    {

        if (curMoney >= pricePerHealth)
        {
            stats.maxHealth++;
            stats.Coins -= pricePerHealth;
        }
        else gamecontrol.NotMoney();
    }
    public void AddDamage()
    {
        if (curMoney >= pricePerDamage)
        {
            player.damage++;
            stats.Coins -= pricePerDamage;
        }
        else gamecontrol.NotMoney();
    }
    public void AddSpeed()
    {
        if (curMoney >= pricePerSpeed)
        {
            player.maxSpeed += 0.05f;
            stats.Coins -= pricePerSpeed;
        }
        else gamecontrol.NotMoney();
    }
    public void AddAttackRange()
    {
        if (curMoney >= pricePerAttackRange)
        {
            player.attackRange += 0.01f;
            stats.Coins -= pricePerAttackRange;
        }
        else gamecontrol.NotMoney();
    }
    
   

}
