using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectebleItems : MonoBehaviour
{
    private HeroStats player;
    bool isTaken = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (!gameObject.GetComponent<CollectebleItems>().isTaken)
            {
                isTaken = true;
                if (gameObject.tag == "Diamond")
                {
                    player.Diamonds ++;
                    Destroy(gameObject);
                }
                else if (gameObject.tag == "Emerald")
                {
                    player.Emeralds++;
                    Destroy(gameObject);
                }
            }

        }
    }
}
