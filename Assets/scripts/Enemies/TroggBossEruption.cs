using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroggBossEruption : MonoBehaviour
{
    public TroggBoss boss;
    private float damageCounter;
    private float damageCD = 0.05f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && boss.eruptionDurationCounter > 0 && boss.eruptionDurationCounter != 10)
        {
            if (damageCounter <= 0)
            {
                Debug.Log("damag");
                collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(boss.eruptionDamage);
            } 
        }
    }

    private void Update()
    {
        if (damageCounter > 0)
        {
            damageCounter -= Time.deltaTime;
        }
        else if (damageCounter < 0)
        {
            damageCounter = damageCD;
        }
    }

    private void Start()
    {
        damageCounter = damageCD;
    }
}
