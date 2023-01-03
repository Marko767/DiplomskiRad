using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;
    private float speed = 10;

    private int MaxHP = 25;
    private int currentHP;

    public HpBAR hpBAR;

    private float attackCD = 3f;
    private float attackCounter;
    private float dashDuration = 1f;
    private float dashDurationCounter;

    private Vector3 direction;
    private int damage = 20;
    private bool stop = false;

    public SpriteRenderer sprite;

    // Update is called once per frame
    void Update()
    {
        hpBAR.SetHealth(currentHP);

        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (attackCounter > 0 && distanceToPlayer < 10)
        {
            attackCounter -= Time.deltaTime;
        }
        if (attackCounter <= 0 )
        {
            stop = false;
            direction = (player.transform.position - transform.position).normalized;

            if(direction.x > 0)
            {
                sprite.flipX = false;
            }
            if (direction.x < 0)
            {
                sprite.flipX = true;
            }

            attackCounter = attackCD;
            dashDurationCounter = dashDuration;
        }
        if(dashDurationCounter > 0 && dashDurationCounter != 10f)
        {
            dashDurationCounter -= Time.deltaTime;
            if(stop == false)
            {
                gameObject.transform.position += direction * speed * Time.deltaTime;
            }
        }
        if (dashDurationCounter <= 0)
        {
            dashDurationCounter = 10f;
        }
    }

    void Start()
    {
        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);

        attackCounter = attackCD;
        dashDurationCounter = 10f;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            stop = true;
        }

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    }

}
