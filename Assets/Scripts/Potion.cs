using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int heal = 30;
    private HeroStats player;
    bool isTaken = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (player.currentHealth < player.maxHealth )
            {
                if (!isTaken)
                {
                    isTaken = true;
                    if (heal > player.maxHealth - player.currentHealth)
                    {
                        player.currentHealth += player.maxHealth - player.currentHealth;
                    }
                    else
                    {
                        player.currentHealth += heal;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
