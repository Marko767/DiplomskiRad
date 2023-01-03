using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBoss : MonoBehaviour
{
    public GameObject player;

    private float attackCD = 5f;
    private float attackCounter;

    private int MaxHPupper = 100;
    private int MaxHPlower = 100;
    private int currentUpperHP;
    private int currentLowerHP;
    private int currentHP;

    public HpBAR hpBAR;

    public GameObject summWasp;
    public GameObject ball;
    public GameObject upperEffects;
    public GameObject lowerEffects;

    private GameObject currentEffect;

    public Transform ballSpawnPosition;

    private float screenAttackBlink = 0.5f;
    private float screenAttackBlinkCounter;
    private float screenAttackAfterBlink = 1f;
    private float screenAttackAfterBlinkCounter;
    private float screenAttackDuration = 1.5f;
    public float screenAttackDurationCounter;

    public int bouncyDamage = 15;
    public int screenAttackDamage = 30;

    public GameObject Chest;
    public GameObject Portal;

    void Start()
    {
        attackCounter = attackCD;

        currentUpperHP = MaxHPupper;
        currentLowerHP = MaxHPlower;

        currentHP = MaxHPlower + MaxHPupper;
        hpBAR.SetMaxHP(MaxHPlower + MaxHPupper);

        screenAttackBlinkCounter = 10;
        screenAttackAfterBlinkCounter = 10;
        screenAttackDurationCounter = 10;
}

    private void Update()
    {
        hpBAR.SetHealth(currentHP);

        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else if (attackCounter <= 0)
        {
            attackCounter = attackCD;
            Attack();
        }

        // Screen attack
        CheckScreenAttackTimers();
    }

    private void Attack()
    {
        int attackRandom = Random.Range(1, 3);
        if (attackRandom == 0)
        {
            HalfScreenAttack();
        }
        else if (attackRandom == 1)
        {
            SummonBouncyBall();
        }
        else if (attackRandom == 2)
        {
            SummonWasp();
        }
    }

    private void HalfScreenAttack()
    {
        int r = Random.Range(0, 2);
        if(r == 0)
        {
            currentEffect = lowerEffects;
        }
        else if(r == 1)
        {
            currentEffect = upperEffects;
        }

        currentEffect.SetActive(true);
        screenAttackBlinkCounter = screenAttackBlink;
    }

    private void SummonBouncyBall()
    {
        GameObject projectileShot = Instantiate(ball, ballSpawnPosition.position, Quaternion.identity);
        Vector3 direction = new Vector3(-1f, Random.Range(-1.0f, 1.0f), 0f);
        projectileShot.GetComponent<BouncyBall>().SetDamageAndTarget(bouncyDamage, direction);
        projectileShot.GetComponent<Rigidbody2D>().velocity = direction * 5;
    }

    private void SummonWasp()
    {
        float x = Random.Range(-15f, 5f);
        float y = Random.Range(-3f, 5f);
        GameObject wasp = Instantiate(summWasp, new Vector3(x, y, 0), Quaternion.identity);
        wasp.GetComponent<Wasp>().LocatePlayer(player);
    }

    public void TakeDamage(int damage)
    {
        if(player.transform.position.y >= 0)
        {
            currentUpperHP -= damage;
            if (currentUpperHP < 0)
            {
                currentUpperHP = 0;
            }
        }
        else if(player.transform.position.y < 0)
        {
            currentLowerHP -= damage;
            if (currentLowerHP < 0)
            {
                currentLowerHP = 0;
            }
        }

        currentHP = currentLowerHP + currentUpperHP;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("died");
        Destroy(gameObject);
        Chest.SetActive(true);
        Portal.SetActive(true);
    }

    private void CheckScreenAttackTimers()
    {
        if (screenAttackBlinkCounter > 0 && screenAttackBlinkCounter != 10)
        {
            screenAttackBlinkCounter -= Time.deltaTime;
        }
        else if (screenAttackBlinkCounter <= 0)
        {
            screenAttackBlinkCounter = 10;
            currentEffect.SetActive(false);
            screenAttackAfterBlinkCounter = screenAttackAfterBlink;
        }

        if (screenAttackAfterBlinkCounter > 0 && screenAttackAfterBlinkCounter != 10)
        {
            screenAttackAfterBlinkCounter -= Time.deltaTime;
        }
        else if (screenAttackAfterBlinkCounter <= 0)
        {
            screenAttackAfterBlinkCounter = 10;
            currentEffect.SetActive(true);
            screenAttackDurationCounter = screenAttackDuration;
        }

        if (screenAttackDurationCounter > 0 && screenAttackDurationCounter != 10)
        {
            screenAttackDurationCounter -= Time.deltaTime;
        }
        else if (screenAttackDurationCounter <= 0)
        {
            screenAttackDurationCounter = 10;
            currentEffect.SetActive(false);
        }
    }
}
