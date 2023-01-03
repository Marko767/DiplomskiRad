using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroggBoss : MonoBehaviour
{
    public GameObject player;

    private float attackCD = 5f;
    private float attackCounter;

    private int MaxHP = 200;
    private int currentHP;

    public HpBAR hpBAR;

    public GameObject slamEffect;
    public GameObject eruptionEffects;

    private bool inAnimantion = false;

    private (float, float) directions = (-1f, 1f);
    private Vector3 direction;
    private float normalSpeed = 1.5f;
    private float speed = 1.5f;

    private float chargeDuration = 0.2f;
    private float chargeDurationCounter;
    private float eruptitonDuration = 2f;
    public float eruptionDurationCounter;
    private float slamDuration = 0.5f;
    public float slamDurationCounter;

    private float inAnimationCounter;

    private bool inCharge = false;
    private bool inSlam = false;
    private bool inEnruption = false;

    public int chargeDamage = 40;
    public int slamDamage = 30;
    public int eruptionDamage = 8;

    public TroggAni animationControl;

    public GameObject Chest;
    public GameObject Portal;

    private bool alive;
    void Start()
    {
        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);

        attackCounter = attackCD;
        chargeDurationCounter = 10;
        eruptionDurationCounter = 10;
        slamDurationCounter = 10;
        inAnimationCounter = 10;
        alive = true;
    }

    private void Update()
    {
        hpBAR.SetHealth(currentHP);
        if (alive)
        {
            if (inAnimantion == false)
            {
                DirectionToPlayer();
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            }

            if (inCharge)
            {
                CheckCharge();
            }
            if (inEnruption)
            {
                CheckEruption();
            }
            if (inSlam)
            {
                CheckSlam();
            }
            if (attackCounter > 0)
            {
                attackCounter -= Time.deltaTime;
            }
            else if (attackCounter <= 0)
            {
                attackCounter = attackCD;
                Attack();
            }
        }
        
    }

    private void Attack()
    {
        int attackRandom = Random.Range(0, 3);
        Debug.Log("attack");
        if (attackRandom == 0)
        {
            Eruption();
        }
        else if (attackRandom == 1)
        {
            Eruption();
        }
        else if (attackRandom == 2)
        {
            Slam();
        }
    }

    private void Eruption()
    {
        inAnimationCounter = 1.5f;
        animationControl.SetCharacterState("erupt");
        inEnruption = true;
    }



    private void Charge()
    {
        inAnimationCounter = 1.5f;
        inCharge = true;
    }

    private void Slam()
    {
        inAnimationCounter = 1.5f;
        animationControl.SetCharacterState("stomp");
        inSlam = true;
    }

    private void CheckCharge()
    {
        if (inAnimationCounter > 0 && inAnimationCounter != 10)
        {
            speed = 0f;
            inAnimationCounter -= Time.deltaTime;
        }
        else if (inAnimationCounter <= 0)
        {
            speed = normalSpeed;
            inAnimationCounter = 10;
            chargeDurationCounter = chargeDuration;
        }

        if (chargeDurationCounter > 0 && chargeDurationCounter != 10)
        {
            speed = 50;
            chargeDurationCounter -= Time.deltaTime;
        }
        else if (chargeDurationCounter <= 0)
        {
            speed = normalSpeed;
            inCharge = false;
            chargeDurationCounter = 10;
        }
    }

    private void CheckEruption()
    {
        if (inAnimationCounter > 0 && inAnimationCounter != 10)
        {
            speed = 0f;
            inAnimationCounter -= Time.deltaTime;
        }
        else if (inAnimationCounter <= 0)
        {
            speed = normalSpeed;
            inAnimationCounter = 10;
            eruptionDurationCounter = eruptitonDuration;
        }

        if (eruptionDurationCounter > 0 && eruptionDurationCounter != 10)
        {
            eruptionEffects.SetActive(true);
            speed = 0;
            eruptionDurationCounter -= Time.deltaTime;
        }
        else if (eruptionDurationCounter <= 0)
        {
            speed = normalSpeed;
            eruptionEffects.SetActive(false);
            inEnruption = false;
            eruptionDurationCounter = 10;
        }
    }

    private void CheckSlam()
    {
        if (inAnimationCounter > 0 && inAnimationCounter != 10)
        {
            speed = 0f;
            inAnimationCounter -= Time.deltaTime;
        }
        else if (inAnimationCounter <= 0)
        {
            speed = normalSpeed;
            inAnimationCounter = 10;
            slamDurationCounter = slamDuration;
        }

        if (slamDurationCounter > 0 && slamDurationCounter != 10)
        {
            slamEffect.SetActive(true);
            speed = 0;
            slamDurationCounter -= Time.deltaTime;
        }
        else if (slamDurationCounter <= 0)
        {
            speed = normalSpeed;
            inSlam = false;
            slamEffect.SetActive(false);
            slamDurationCounter = 10;
        }
    }

    private void DirectionToPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            direction = new Vector3(directions.Item1, 0, 0);
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            direction = new Vector3(directions.Item2, 0, 0);
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animationControl.SetCharacterState("die");
        alive = false;
        Chest.SetActive(true);
        Portal.SetActive(true);
    }
}
