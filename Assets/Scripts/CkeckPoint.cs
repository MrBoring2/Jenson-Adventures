using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CkeckPoint : MonoBehaviour
{
    public bool isChecked = false;
    public GameController game;
    private HeroStats heroStats;
    public float offsetX;
    public float offsetY = 2f;
    public DeathScreen deathScreen;
    void Start()
    {
        heroStats = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {

            if (isChecked == false)
            {
                deathScreen.checkPointButton.gameObject.SetActive(true);
                coll.gameObject.GetComponent<HeroStats>().RespawnPosition = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
                coll.gameObject.GetComponent<HeroStats>().SaveHealth = coll.gameObject.GetComponent<HeroStats>().currentHealth;
                
                game.CheckPoint();
                isChecked = true;
            }
        }
    }
}
