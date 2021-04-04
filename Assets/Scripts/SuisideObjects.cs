using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuisideObjects : MonoBehaviour
{
    private HeroStats player;
    bool isTrigger = false;
    public DeathScreen deathScreen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (!gameObject.GetComponent<SuisideObjects>().isTrigger)
            {
                isTrigger = true;
                player.IsDead = true;
                player.TakeDamage(player.currentHealth);
                player.healthText.text = player.currentHealth.ToString();
                player.healthBar.SetHealth(player.currentHealth);
               

            }

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(gameObject.GetComponent<SuisideObjects>().isTrigger)
            isTrigger = false;
    }
}
