using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroggBossSlam : MonoBehaviour
{
    public TroggBoss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && boss.slamDurationCounter > 0 && boss.slamDurationCounter != 10)
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(boss.slamDamage);
        }
    }
}
