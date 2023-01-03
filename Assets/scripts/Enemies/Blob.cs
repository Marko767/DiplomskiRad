using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;

    private float jumpCD = 2f;
    private float jumpCounter;
    private float jumpDuration = 1f;
    private float jumpDurCounter = 100f;

    private (float, float) jumpSide = (-3f, 3f);

    private Rigidbody2D rb;

    private int MaxHP = 100;
    private int currentHP;

    public HpBAR hpBAR;

    private int damage = 20;

    public SpriteRenderer sprite;

    void Update()
    {
        hpBAR.SetHealth(currentHP);

        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (jumpCounter > 0 && distanceToPlayer < 10)
        {
            jumpCounter -= Time.deltaTime;
        }
        else if (jumpCounter <= 0)
        {
            jumpCounter = jumpCD;
            BlobJump();
        }
        if (jumpDurCounter > 0 && jumpDurCounter != 100f)
        {
            jumpDurCounter -= Time.deltaTime;
        }
        else if (jumpDurCounter <= 0)
        {
            BlobStopJump();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCounter = jumpCD;

        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);
    }

    private void BlobJump()
    {
        float dir = 0f;
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            dir = jumpSide.Item1;
        }
        else if(rand == 1)
        {
            dir = jumpSide.Item2;
        }

        if(dir == jumpSide.Item1)
        {
            sprite.flipX = true; 
        }

        if (dir == jumpSide.Item2)
        {
            sprite.flipX = false;
        }

        rb.velocity = new Vector2(dir, 5);
        jumpDurCounter = jumpDuration;
    }

    private void BlobStopJump()
    {
        rb.velocity = new Vector2(0, 0);
        jumpDurCounter = 100f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumpDurCounter > 0 && jumpDurCounter != 100f)
        {
            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            }
        }    
    }
}
