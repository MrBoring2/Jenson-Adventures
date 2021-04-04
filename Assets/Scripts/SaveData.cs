using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public float maxspeed;
    public int maxhealth;
    public int damage;
    public float attackRadius;

    public SaveData (Player player, HeroStats heroStats)
    {
        maxhealth = heroStats.maxHealth;
        maxspeed = player.maxSpeed;
        damage = player.damage;
        attackRadius = player.attackRange;
    }
}
