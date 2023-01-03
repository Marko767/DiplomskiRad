using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;

    private float jumpCD = 3f;
    private float jumpCounter;

    private float vertcalJumpDuration = 1f;
    private float verticalJumpDurCounter = 100f;

    private float horizontalJumpDuration = 0.2f;
    private float horizontalJumpDurCounter = 100f;

    private float attackCD = 2f;
    private float attackCounter;
    public GameObject projectile;

    private Rigidbody2D rb;

    private int MaxHP = 60;
    private int currentHP;

    public HpBAR hpBAR;

    private int damage = 10;

    public ArcherAni animationControl;

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
            VerticalJump();
        }

        if (attackCounter > 0 && distanceToPlayer < 18)
        {
            attackCounter -= Time.deltaTime;
        }
        else if (attackCounter <= 0)
        {
            attackCounter = attackCD;
            Shoot();
        }

        if (verticalJumpDurCounter > 0 && verticalJumpDurCounter != 100f)
        {
            verticalJumpDurCounter -= Time.deltaTime;
        }
        else if (verticalJumpDurCounter <= 0)
        {
            HorizontalJump();
        }

        if (horizontalJumpDurCounter > 0 && horizontalJumpDurCounter != 100f)
        {
            horizontalJumpDurCounter -= Time.deltaTime;
        }
        else if (horizontalJumpDurCounter <= 0)
        {
            StopJump();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCounter = jumpCD;
        attackCounter = attackCD;

        currentHP = MaxHP;
        hpBAR.SetMaxHP(MaxHP);
    }

    private void VerticalJump()
    {
        rb.velocity = new Vector2(0, 10);
        verticalJumpDurCounter = vertcalJumpDuration;
        animationControl.SetCharacterState("jump");
    }

    private void HorizontalJump()
    {
        verticalJumpDurCounter = 100f;
        rb.velocity = new Vector2(0, 0);
        rb.velocity = new Vector2(20, 0);
        horizontalJumpDurCounter = horizontalJumpDuration;
        animationControl.SetCharacterState("dash");
    }

    private void StopJump()
    {
        rb.velocity = new Vector2(0, 0);
        horizontalJumpDurCounter = 100f;
        animationControl.SetCharacterState("idle");
    }

    private void Shoot()
    {
        GameObject projectileShot = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        projectileShot.GetComponent<ArcherShot>().SetDamageAndTarget(damage, direction);
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
