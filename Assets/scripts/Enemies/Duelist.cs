using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duelist : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;

    private float dirChangeCD = 2f;
    private float dirChangeCounter;

    private float attackCD = 1.5f;
    private float attackCounter;

    private (float, float) directions = (-1f, 1f);
    private Vector3 direction;

    private int MaxHP = 100;
    private int currentHP;

    public HpBAR hpBAR;

    private int damage = 20;
    private float speed = 1.5f;

    public DuelistAni animationControl;

    void Start()
    {
        attackCounter = attackCD;
        dirChangeCounter = dirChangeCD;

        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);
    }

    void Update()
    {
        hpBAR.SetHealth(currentHP);

        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (dirChangeCounter > 0)
        {
            dirChangeCounter -= Time.deltaTime;
        }
        else if (dirChangeCounter <= 0 && distanceToPlayer < 10)
        {
            dirChangeCounter = dirChangeCD;
            DirectionToPlayer();
        }
        else if (dirChangeCounter <= 0 && distanceToPlayer >= 10)
        {
            dirChangeCounter = dirChangeCD;
            RandomDirection();
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else if (attackCounter <= 0 && distanceToPlayer < 1)
        {
            animationControl.SetCharacterState("attack");
            Attack();
            attackCounter = attackCD;
        }
    }

    private void DirectionToPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            direction = new Vector3(directions.Item1, 0, 0);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (transform.position.x < player.transform.position.x)
        {
            direction = new Vector3(directions.Item2, 0, 0);
            transform.localScale = new Vector2(1f, 1f);
        }

        speed = 3.5f;
        dirChangeCounter = dirChangeCD;
    }

    private void RandomDirection()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            direction = new Vector3(directions.Item1, 0, 0);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (rand == 1)
        {
            direction = new Vector3(directions.Item2, 0, 0);
            transform.localScale = new Vector2(1f, 1f);
        }

        speed = 1.5f;
        dirChangeCounter = dirChangeCD;
    }

    private void Attack()
    {
        player.GetComponent<PlayerCombat>().TakeDamage(damage);
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
        Destroy(gameObject);
    }
}
