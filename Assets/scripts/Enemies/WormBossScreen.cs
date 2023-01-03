using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBossScreen : MonoBehaviour
{
    public WormBoss boss;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && boss.screenAttackDurationCounter > 0 && boss.screenAttackDurationCounter != 10)
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(boss.screenAttackDamage);
        }
    }
}
