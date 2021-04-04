using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int price;
    public Sprite coinImage;
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
            if (!gameObject.GetComponent<Coin>().isTaken)
            {
                isTaken = true;
                player.Coins += price;
                player.coinImage.sprite = coinImage;
                Destroy(gameObject);
            }

        }
    }
}
